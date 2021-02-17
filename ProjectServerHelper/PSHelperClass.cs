using Microsoft.ProjectServer.Client;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint.Client.WorkflowServices;
using System.IO;

namespace ProjectServerHelper
{
    public class PSHelperClass
    {
        private string _UserName;
        private string _Password;
        private string _PWAUrl;

        private static PSHelperClass _psInstance;

        private PSHelperClass()
        {
        }

        public static PSHelperClass CreateInstance(string UserName, string Password, string PWAUrl)
        {
            _psInstance = new PSHelperClass();
            _psInstance._UserName = UserName;
            _psInstance._Password = Password;
            _psInstance._PWAUrl = PWAUrl;
            return _psInstance;
        }

        public static PSHelperClass GetInstance()
        {
            if (_psInstance == null)
                _psInstance = new PSHelperClass();

            return _psInstance;
        }

        public List<PSHPDP> GetAllPDP()
        {
            List<PSHPDP> lstPDP = new List<PSHPDP>();
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                var projectContext = new ProjectServer(clientContext);

                var objPDPCollection = projectContext.ProjectDetailPages;
                clientContext.Load(objPDPCollection);
                clientContext.ExecuteQuery();
                if (objPDPCollection.AreItemsAvailable)
                {
                    foreach (ProjectDetailPage oPage in objPDPCollection)
                    {
                        lstPDP.Add(new PSHPDP()
                        {
                            PDPName = oPage.Name,
                            PDPGuid = oPage.Id
                        });
                    }
                }
            }

            return lstPDP;
        }

        public List<PSHStage> GetAllStages()
        {
            List<PSHStage> lstStages = new List<PSHStage>();
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                var projectContext = new ProjectServer(clientContext);

                //var objPDPCollection = projectContext.ProjectDetailPages;
                clientContext.Load(projectContext.Stages);
                clientContext.ExecuteQuery();
                foreach (Stage oStage in projectContext.Stages)
                {
                    lstStages.Add(new PSHStage()
                    {
                        StageGuid = oStage.Id,
                        StageName = oStage.Name
                    });
                }
            }

            return lstStages;
        }

        public List<PSHPhase> GetAllPhases()
        {
            List<PSHPhase> lstPhases = new List<PSHPhase>();
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                var projectContext = new ProjectServer(clientContext);

                //var objPDPCollection = projectContext.ProjectDetailPages;
                clientContext.Load(projectContext.Phases);
                clientContext.ExecuteQuery();
                foreach (Phase oPhase in projectContext.Phases)
                {
                    lstPhases.Add(new PSHPhase()
                    {
                        PhaseGuid = oPhase.Id,
                        PhaseName = oPhase.Name
                    });
                }
            }

            return lstPhases;
        }

        public string AddPDP2Stage(List<PDPStageMapping> Mapping)
        {
            string success = string.Empty;
            try
            {
                using (var clientContext = new ClientContext(_PWAUrl))
                {
                    clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                    var projectContext = new ProjectServer(clientContext);

                    var objPDPCollection = projectContext.ProjectDetailPages;
                    clientContext.Load(objPDPCollection);
                    clientContext.Load(projectContext.Stages);
                    clientContext.ExecuteQuery();

                    if (objPDPCollection.AreItemsAvailable)
                    {
                        foreach (PDPStageMapping objmap in Mapping)
                        {
                            ProjectDetailPage objNewPage = objPDPCollection.GetByGuid(objmap.PDPId);
                            Stage stage = projectContext.Stages.GetByGuid(objmap.StageId);
                            clientContext.Load(objNewPage);
                            clientContext.Load(stage);
                            clientContext.ExecuteQuery();

                            clientContext.Load(stage.ProjectDetailPages);
                            clientContext.ExecuteQuery();

                            Guid pdpGuid = objNewPage.Id;
                            int objOldStageDetail = stage.ProjectDetailPages.Where(x => x.Id == pdpGuid).Count();

                            if (objOldStageDetail == 0)
                            {
                                StageDetailPageCreationInformation objPDPCreationInfo = new StageDetailPageCreationInformation();
                                objPDPCreationInfo.Id = pdpGuid;
                                objPDPCreationInfo.Position = 1;
                                objPDPCreationInfo.RequiresAttention = false;
                                objPDPCreationInfo.Description = objNewPage.Name;

                                StageDetailPage objStageDetail = stage.ProjectDetailPages.Add(objPDPCreationInfo);

                                projectContext.Stages.Update();
                                clientContext.ExecuteQuery();
                                success = "Success"; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = ex.Message;
            }
            return success;
        }

        public string AddStage(string StageName, string Description, Guid PhaseId, Guid WorkflowStatusPageId,
            List<PSHPDP> listPDPs)
        {
            string success = string.Empty;
            try
            {
                using (var clientContext = new ClientContext(_PWAUrl))
                {
                    clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                    var projectContext = new ProjectServer(clientContext);

                    var objPDPCollection = projectContext.ProjectDetailPages;
                    clientContext.Load(objPDPCollection);
                    clientContext.Load(projectContext.Stages);
                    clientContext.ExecuteQuery();

                    List<StageDetailPageCreationInformation> listPDPCreationInfo = new List<StageDetailPageCreationInformation>();
                    foreach (PSHPDP objPDP in listPDPs)
                    {
                        StageDetailPageCreationInformation objPDPCreationInfo = new StageDetailPageCreationInformation();
                        objPDPCreationInfo.Id = objPDP.PDPGuid;
                        objPDPCreationInfo.Position = 1;
                        objPDPCreationInfo.RequiresAttention = false;
                        objPDPCreationInfo.Description = objPDP.PDPName;

                        listPDPCreationInfo.Add(objPDPCreationInfo);
                    }

                    StageCreationInformation objStgCreation = new StageCreationInformation();
                    objStgCreation.Name = StageName;
                    objStgCreation.Description = Description;
                    objStgCreation.PhaseId = PhaseId;
                    objStgCreation.WorkflowStatusPageId = WorkflowStatusPageId;
                    objStgCreation.ProjectDetailPages = listPDPCreationInfo;

                    projectContext.Stages.Add(objStgCreation);
                    projectContext.Stages.Update();
                    clientContext.ExecuteQuery();
                    success = "Success";
                }
            }
            catch (Exception ex)
            {
                success = ex.Message;
            }
            return success;
        }

        public string AddPhase(string PhaseName, string Description)
        {
            string success = string.Empty;
            try
            {
                using (var clientContext = new ClientContext(_PWAUrl))
                {
                    clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                    var projectContext = new ProjectServer(clientContext);

                    clientContext.Load(projectContext.Phases);
                    clientContext.ExecuteQuery();

                    PhaseCreationInformation objPhaseCreation = new PhaseCreationInformation();
                    objPhaseCreation.Name = PhaseName;
                    objPhaseCreation.Description = Description;

                    projectContext.Phases.Add(objPhaseCreation);
                    projectContext.Phases.Update();
                    clientContext.ExecuteQuery();
                    success = "Success";
                }
            }
            catch (Exception ex)
            {
                success = ex.Message;
            }
            return success;
        }

        public string CreatePDP(string PDPName, string PDPTitle, string PDPDescription, string PageType)
        {
            string success = string.Empty;
            try
            {
                using (var clientContext = new ClientContext(_PWAUrl))
                {
                    clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                    //var projectContext = new ProjectServer(clientContext);

                    Web web = clientContext.Web;

                    //var objPDPCollection = projectContext.ProjectDetailPages;
                    //clientContext.Load(objPDPCollection);
                    //clientContext.ExecuteQuery();

                    var PDPPageLib = web.Lists.GetByTitle("Project Detail Pages");
                    clientContext.Load(PDPPageLib);
                    clientContext.ExecuteQuery();

                    string strTemplatePath = Environment.CurrentDirectory + "PDPTemplate.aspx";
                    FileCreationInformation objFileInfo = new FileCreationInformation();
                    objFileInfo.Url = PDPName + ".aspx";

                    //StreamReader pdpTemplate = new StreamReader(strTemplatePath);
                    //string fileContent = pdpTemplate.ReadToEnd();
                    //pdpTemplate.Close();

                    objFileInfo.Content = System.IO.File.ReadAllBytes(strTemplatePath);
                    Microsoft.SharePoint.Client.File file = PDPPageLib.RootFolder.Files.Add(objFileInfo);
                    clientContext.Load(file);
                    clientContext.ExecuteQuery();

                    // Set Page Title and Publish Page
                    ListItem pageItem = file.ListItemAllFields;
                    pageItem["Display Name"] = PDPTitle;
                    pageItem["Description"] = PDPDescription;
                    pageItem["Page Type"] = PageType;
                    pageItem.Update();
                    
                    pageItem.File.CheckIn(String.Empty, CheckinType.MajorCheckIn);
                    clientContext.ExecuteQuery();
                    success = "Success";
                }
            }
            catch (Exception ex)
            {
                success = ex.Message;
            }
            return success;
        }

        public void GetAllProjects()
        {
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                clientContext.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                var projectContext = new ProjectServer(clientContext);

                clientContext.Load(projectContext.Projects);
                clientContext.ExecuteQuery();
            }
        }

        public List<PSHWorkflowDefinition> GetWorkflowDefinitions()
        {
            List<PSHWorkflowDefinition> lstWFDefinitions = new List<PSHWorkflowDefinition>();
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                var workflowServicesManager = new WorkflowServicesManager(clientContext, clientContext.Web);

                // connect to the deployment service
                var workflowDeploymentService = workflowServicesManager.GetWorkflowDeploymentService();
                Web oWeb = clientContext.Web;

                // get all installed workflows
                var publishedWorkflowDefinitions = workflowDeploymentService.EnumerateDefinitions(true);
                clientContext.Load(publishedWorkflowDefinitions);
                clientContext.ExecuteQuery();
                foreach (var pubwfDef in publishedWorkflowDefinitions)
                {
                    Console.WriteLine("Name:{0} \nID: {1}", pubwfDef.DisplayName, pubwfDef.Id.ToString());
                    Console.WriteLine("---------------------------------------");

                    PSHWorkflowDefinition objWFDefinition = new PSHWorkflowDefinition()
                    {
                        WFDefinitionId = pubwfDef.Id,
                        WFDefinitionXAML = pubwfDef.Xaml,
                        WFName = pubwfDef.DisplayName
                    };

                    lstWFDefinitions.Add(objWFDefinition);
                }
            }
            return lstWFDefinitions;
        }

        public PSHWorkflowDefinition GetWorkflowDefinition(string WFDefinitionName)
        {
            PSHWorkflowDefinition objWFDefinition = null;
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                var workflowServicesManager = new WorkflowServicesManager(clientContext, clientContext.Web);

                // connect to the deployment service
                var workflowDeploymentService = workflowServicesManager.GetWorkflowDeploymentService();
                Web oWeb = clientContext.Web;
                
                //Get all the definitions from the Deployment Service, or get a specific definition using the GetDefinition method.
                WorkflowDefinitionCollection wfDefinitions = workflowDeploymentService.EnumerateDefinitions(false);
                clientContext.Load(wfDefinitions, wfDefs => wfDefs.Where(wfd => wfd.DisplayName == WFDefinitionName));
                clientContext.ExecuteQuery();

                WorkflowDefinition wfDefinition = wfDefinitions.First();

                ClientResult<Guid> wfDefId = workflowDeploymentService.SaveDefinition(wfDefinition);
                workflowDeploymentService.PublishDefinition(wfDefinition.Id);
                clientContext.ExecuteQuery();

                if (wfDefinition != null)
                {
                    objWFDefinition = new PSHWorkflowDefinition()
                    {
                        WFDefinitionId = wfDefinition.Id,
                        WFDefinitionXAML = wfDefinition.Xaml,
                        WFName = wfDefinition.DisplayName
                    };
                }                
            }

            return objWFDefinition;
        }

        public void UpdateWorkflowDefinitionXAML(PSHWorkflowDefinition WFDefinition)
        {
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                var workflowServicesManager = new WorkflowServicesManager(clientContext, clientContext.Web);

                // connect to the deployment service
                var workflowDeploymentService = workflowServicesManager.GetWorkflowDeploymentService();

                //Get all the definitions from the Deployment Service, or get a specific definition using the GetDefinition method.
                WorkflowDefinitionCollection wfDefinitions = workflowDeploymentService.EnumerateDefinitions(false);
                clientContext.Load(wfDefinitions, wfDefs => wfDefs.Where(wfd => wfd.Id == WFDefinition.WFDefinitionId));
                clientContext.ExecuteQuery();

                WorkflowDefinition wfDefinition = wfDefinitions.First();
                wfDefinition.DisplayName = WFDefinition.WFName;
                wfDefinition.Xaml = WFDefinition.WFDefinitionXAML;

                ClientResult<Guid> wfDefId = workflowDeploymentService.SaveDefinition(wfDefinition);
                workflowDeploymentService.PublishDefinition(wfDefinition.Id);
                clientContext.ExecuteQuery();
            }
        }

        public List<PSHWFAssociation> GetWorkflowAssociations(PSHWorkflowDefinition WFDefinition)
        {
            List<PSHWFAssociation> lstWFAssociations = new List<PSHWFAssociation>();
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                var workflowServicesManager = new WorkflowServicesManager(clientContext, clientContext.Web);
                //connect to the subscription service
                var workflowSubscriptionService = workflowServicesManager.GetWorkflowSubscriptionService();

                var workflowAssociations = workflowSubscriptionService.EnumerateSubscriptionsByDefinition(WFDefinition.WFDefinitionId);
                clientContext.Load(workflowAssociations);
                clientContext.ExecuteQuery();

                if (workflowAssociations.AreItemsAvailable)
                {
                    foreach (WorkflowSubscription ws in workflowAssociations)
                    {
                        PSHWFAssociation objWFDefinition = new PSHWFAssociation()
                        {
                            WFDefinitionId = ws.DefinitionId,
                            WFAssoId = ws.Id,
                            WFAssoName = ws.Name,
                            WFAssoEventTypes = ws.EventTypes,
                            WFAssoPropertyDefinitions   = ws.PropertyDefinitions
                        };

                        lstWFAssociations.Add(objWFDefinition);
                    }
                }                
            }
            return lstWFAssociations;
        }

        public void CreateWorkflowAssociation(PSHWFAssociation WFAssociation)
        {
            using (var clientContext = new ClientContext(_PWAUrl))
            {
                var workflowServicesManager = new WorkflowServicesManager(clientContext, clientContext.Web);
                //connect to the subscription service
                var workflowSubscriptionService = workflowServicesManager.GetWorkflowSubscriptionService();

                var workflowAssociations = workflowSubscriptionService.EnumerateSubscriptionsByDefinition(WFAssociation.WFDefinitionId);
                clientContext.Load(workflowAssociations);
                clientContext.ExecuteQuery();

                if (workflowAssociations.AreItemsAvailable)
                {
                    foreach (WorkflowSubscription ws in workflowAssociations)
                    {
                        if (ws.Id == WFAssociation.WFAssoId)
                        {
                            ws.SetProperty("PrepareBriefValue", "06c07e59-cfce-e411-8e68-2c44fd94c786");

                            // create the association
                            workflowSubscriptionService.PublishSubscription(ws);
                            clientContext.ExecuteQuery();
                        }
                    }
                }
                else
                {
                    // create a new association / subscription
                    WorkflowSubscription newSubscription = new WorkflowSubscription(clientContext)
                    {
                        DefinitionId = WFAssociation.WFDefinitionId,
                        Enabled = true,
                        Name = WFAssociation.WFAssoName
                    };

                    //workflowHistoryListId = historyList.Id;
                    //workflowTaskListId = tasksList.Id;

                    //var startupOptions = new List<string>();
                    //// manual start
                    //startupOptions.Add("WorkflowStart");

                    // set the workflow start settings
                    newSubscription.EventTypes = WFAssociation.WFAssoEventTypes;// startupOptions;

                    // set the associated task and history lists
                    foreach (KeyValuePair<string, string> propertyDef in WFAssociation.WFAssoPropertyDefinitions)
                    {
                        newSubscription.SetProperty(propertyDef.Key, propertyDef.Value);
                    }
                    //newSubscription.SetProperty("HistoryListId",WFAssociation.WFAssoPropertyDefinitions["HistoryListId"]);// workflowHistoryListId.ToString());
                    //newSubscription.SetProperty("TaskListId", WFAssociation.WFAssoPropertyDefinitions["TaskListId"]);// workflowTaskListId.ToString());

                    //// OPTIONAL: add any association form values
                    //newSubscription.SetProperty("PrepareBriefValue", "06c07e59-cfce-e411-8e68-2c44fd94c786");

                    // create the association
                    workflowSubscriptionService.PublishSubscription(newSubscription);
                    clientContext.ExecuteQuery();
                }
            }
        }
    }
}
