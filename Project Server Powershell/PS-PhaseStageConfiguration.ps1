Add-PSSnapin "Microsoft.SharePoint.Powershell.dll" -ErrorAction SilentlyContinue

$pwaSiteCollectionUrl = "http://pwaSiteCollectionUrl"

$LoginName = 'domain\username'
$password = 'password'

Add-Type -Path "c:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\ISAPI\Microsoft.SharePoint.Client.dll" 
Add-Type -Path "c:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\ISAPI\Microsoft.SharePoint.Client.Runtime.dll" 
Add-Type -Path "C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Microsoft.ProjectServer.Client\v4.0_15.0.0.0__71e9bce111e9429c\Microsoft.ProjectServer.Client.dll" 
Add-Type -Path "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Net.dll"

##----------------------------------------------------------------------------------------------
Function GetPDPCreationInfo($csvStagePDP,$PDPCollection,$StageName)
{
    $listPDPCreationInfo = New-Object "System.Collections.Generic.List``1[Microsoft.ProjectServer.Client.StageDetailPageCreationInformation]";
    foreach ($objPDP in $csvStagePDP)
    {
        #write-host  $objPDP.PDPGuid
        if($objPDP.PDPName -ne 'PDPName' -and $objPDP.StageName -eq $StageName){
            $objPDPCreationInfo = New-Object "Microsoft.ProjectServer.Client.StageDetailPageCreationInformation";
            #$objPDPCreationInfo.Id = New-Object System.Guid($objPDP.PDPGuid.Trim());
            
            [System.Guid]$PDPGuid = GetPDPGuid $PDPCollection $objPDP.PDPName
            $objPDPCreationInfo.Id = $PDPGuid;            
            $objPDPCreationInfo.Position = 1;
            $objPDPCreationInfo.RequiresAttention = $false;
            $objPDPCreationInfo.Description = $objPDP.PDPName;

            $listPDPCreationInfo.Add($objPDPCreationInfo);
        }
    }
    return $listPDPCreationInfo
}

Function GetPDPGuid($PDPCollection,$PDPName)
{
    $PDPGuid = $null;
    foreach($myPDP in $PDPCollection)
    {
        if($myPDP.Name -eq $PDPName.Trim())
        {
            $PDPGuid = $myPDP.Id;
            break;
        }
    }
    return $PDPGuid;
}

Function GetPhaseId($PhaseCollection,$PhaseName)
{
    $PhaseGuid = $null;
    foreach($Phase in $PhaseCollection)
    {
        if($Phase.Name -eq $PhaseName)
        {
           $PhaseGuid = $Phase.Id;
           break;
        }
    }
    return $PhaseGuid;
}

##----------------------------------------------------------------------------------------------

$context=New-Object Microsoft.SharePoint.Client.ClientContext($pwaSiteCollectionUrl)
$context.Credentials = New-Object System.Net.NetworkCredential($LoginName, $password)
write-host  "1) Client Context Created." -ForegroundColor Yellow
$site = $context.Web;
$context.Load($site);
$context.ExecuteQuery();


##Creating PDPs
$PDPPageLib = $site.Lists.GetByTitle("Project Detail Pages");
$context.Load($PDPPageLib);
$context.ExecuteQuery();
Write-Host "2) 'Project Detail Pages' Library Loaded into Context." -ForegroundColor Yellow

$DirectoryPath = $PSScriptRoot;

$strTemplatePath = $DirectoryPath + "\PDPTemplate.aspx";

$PDPCSVPath = $DirectoryPath + "\PDPs.csv"
$csvPDP = Import-Csv $PDPCSVPath
Write-Host  "3) Creating PDPs form CSV..." -ForegroundColor Yellow
Foreach($objPDP in $csvPDP){
    $NewPDPName = $objPDP.PDPName;
    Try{
        if($NewPDPName -ne ''){
            

            [Microsoft.SharePoint.Client.FileCreationInformation] $objFileInfo = New-Object Microsoft.SharePoint.Client.FileCreationInformation;
            $objFileInfo.Url = $NewPDPName;

            $objFileInfo.Content = [System.IO.File]::ReadAllBytes($strTemplatePath);
            [Microsoft.SharePoint.Client.File] $file = $PDPPageLib.RootFolder.Files.Add($objFileInfo);
            $context.Load($file);
            $context.ExecuteQuery();

            write-host "$NewPDPName PDP Created..." -ForegroundColor Green


            write-host  "* updating properties..." -ForegroundColor Yellow
            #Set Page Title and Publish Page
            [Microsoft.SharePoint.Client.ListItem] $pageItem = $file.ListItemAllFields;
            $pageItem["TitlePDP"] = $objPDP.PDPDisplayName;
            $pageItem["Description"] = $objPDP.Description;
            $pageItem["PageType"] = $objPDP.PageType;
            $pageItem.Update();
            #$pageItem.File.CheckIn("",[Microsoft.SharePoint.Client.CheckinType]::MajorCheckIn);
            $context.ExecuteQuery();
            write-host  "PDP properties updated..." -ForegroundColor Green
        }
    }
    Catch
    {
        $ErrorMessage = $_.Exception.Message
        Write-Host "Error in Creating $NewPDPName PDP: " $ErrorMessage -ForegroundColor Red
    }
}

write-host  "PDP Creation from CSV Completed." -ForegroundColor Green


$projectContext = New-Object Microsoft.ProjectServer.Client.ProjectServer($context);
write-host  "4) ProjectServer Context Initialized." -ForegroundColor Yellow


$context.Load($projectContext.Phases);
$objPDPCollection = $projectContext.ProjectDetailPages;
$context.Load($objPDPCollection);                
$PSStages = $projectContext.Stages
$context.Load($PSStages);                 
$context.ExecuteQuery();

##Creating Phases
write-host  "5) Creating Phases from CSV..." -ForegroundColor Yellow
$PhaseCSVPath = $DirectoryPath + "\Phases.csv"
$csvPhases = Import-Csv $PhaseCSVPath
##-header PhaseName,PhaseDescription

foreach($objNPhase in $csvPhases)
{
    $NewPhaseName = $objNPhase.PhaseName
    Try{
        
        [Microsoft.ProjectServer.Client.PhaseCreationInformation] $objPhaseCreation = New-Object Microsoft.ProjectServer.Client.PhaseCreationInformation;
        $objPhaseCreation.Name = $NewPhaseName;
        $objPhaseCreation.Description = $objNPhase.PhaseDescription;

        $NewPhase = $projectContext.Phases.Add($objPhaseCreation);
        $context.Load($NewPhase);
        $projectContext.Phases.Update();
        $context.ExecuteQuery();

        Write-Host "$NewPhaseName created successfully..." -ForegroundColor Green
    }
    Catch
    {
        $ErrorMessage = $_.Exception.Message
        Write-Host "Error in Creating $NewPhaseName Phase: " $ErrorMessage -ForegroundColor Red
    }
}
write-host  "Phases creation from CSV Completed." -ForegroundColor Green

##Creating Stages
write-host  "6) Creating Stages from CSV." -ForegroundColor Yellow

$StagesCSVPath = $DirectoryPath + "\Stages.csv"
$csvStages = Import-Csv $StagesCSVPath
##-header StageName,StageDescription,PhaseName,WorkflowStatusPDP

$StagePDPMappingCSVPath = $DirectoryPath + "\StagePDPMapping.csv"
$csvStagePDP = Import-Csv $StagePDPMappingCSVPath
##-header PDPName,StageName

foreach ($objStage in $csvStages)
{
    $NewStageName = $objStage.StageName
    Try{
        

        [Microsoft.ProjectServer.Client.StageCreationInformation]$objStageCreationInfo = New-Object "Microsoft.ProjectServer.Client.StageCreationInformation";
        $objStageCreationInfo.Name = $NewStageName;
        $objStageCreationInfo.Description = $objStage.StageDescription;

        [System.Guid]$PhaseId = GetPhaseId $projectContext.Phases $objStage.PhaseName 
        $objStageCreationInfo.PhaseId = $PhaseId 
    
        [System.Collections.Generic.List``1[Microsoft.ProjectServer.Client.StageDetailPageCreationInformation]]$listPDPCreationInfo = GetPDPCreationInfo $csvStagePDP $objPDPCollection $objStage.StageName
        $objStageCreationInfo.ProjectDetailPages = $listPDPCreationInfo

        $WFStatus = $objStage.WorkflowStatusPDP;
        [System.Guid]$WorkflowStatusPageId =  GetPDPGuid $objPDPCollection $objStage.WorkflowStatusPDP
        $objStageCreationInfo.WorkflowStatusPageId = $WorkflowStatusPageId;

        $NewStage = $PSStages.Add($objStageCreationInfo);
        $context.Load($NewStage);
        $PSStages.Update();
        $context.ExecuteQuery();

        Write-Host "$NewStageName created successfully..." -ForegroundColor Green
    }
    Catch
    {
        $ErrorMessage = $_.Exception.Message
        Write-Host "Error in Creating $NewStageName Stages: " $ErrorMessage -ForegroundColor Red
    }
    
}
write-host  "Stages creation from CSV Completed." -ForegroundColor Green



write-host  "Disposing Client Context." -ForegroundColor Yellow
$context.Dispose();

write-host  "Script Completed successfully!!!" -ForegroundColor Green