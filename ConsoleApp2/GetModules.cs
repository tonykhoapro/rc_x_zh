
using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.CustomViews;
using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.Modules.ModulesOperations;
using ActionHandler = Com.Zoho.Crm.API.Modules.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Modules.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Modules.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Modules.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Modules.BodyWrapper;
using Module = Com.Zoho.Crm.API.Modules.Modules;
using ResponseHandler = Com.Zoho.Crm.API.Modules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Modules.ResponseWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Modules.SuccessResponse;
namespace ConsoleApp2
{
    public class Modules
    {
        /// 
        /// This method is used to get metadata about all the modules and print the response.
        /// 
        public static void GetModules()
        {
            //Get instance of ModulesOperations Class
            ModulesOperations moduleOperations = new ModulesOperations();
            HeaderMap headerInstance = new HeaderMap();
            DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
            //headerInstance.Add(GetModulesHeader.IF_MODIFIED_SINCE, ifModifiedSince);
            //Call GetModules method that takes headerInstance as parameters
            APIResponse<ResponseHandler> response = moduleOperations.GetModules(headerInstance);
            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }
                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;
                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                        //Get the list of obtained Module instances
                        List<Module> modules = responseWrapper.Modules;
                        foreach (Module module in modules)
                        {
                            //Get the Name of each Module
                            Console.WriteLine("Module Name: " + module.ModuleName);
                            //Get the GlobalSearchSupported of each Module
                            Console.WriteLine("Module GlobalSearchSupported: " + module.GlobalSearchSupported);
                            //Get the Deletable of each Module
                            Console.WriteLine("Module Deletable: " + module.Deletable);
                            //Get the Description of each Module
                            Console.WriteLine("Module Description: " + module.Description);
                            //Get the Creatable of each Module
                            Console.WriteLine("Module Creatable: " + module.Creatable);
                            //Get the InventoryTemplateSupported of each Module
                            Console.WriteLine("Module InventoryTemplateSupported: " + module.InventoryTemplateSupported);
                            if (module.ModifiedTime != null)
                            {
                                //Get the ModifiedTime of each Module
                                Console.WriteLine("Module ModifiedTime: " + module.ModifiedTime);
                            }
                            //Get the PluralLabel of each Module
                            Console.WriteLine("Module PluralLabel: " + module.PluralLabel);
                            //Get the PresenceSubMenu of each Module
                            Console.WriteLine("Module PresenceSubMenu: " + module.PresenceSubMenu);
                            //Get the TriggersSupported of each Module
                            Console.WriteLine("Module TriggersSupported: " + module.TriggersSupported);
                            //Get the Id of each Module
                            Console.WriteLine("Module Id: " + module.Id);
                            //Get the Visibility of each Module
                            Console.WriteLine("Module Visibility: " + module.Visibility);
                            //Get the Convertable of each Module
                            Console.WriteLine("Module Convertable: " + module.Convertable);
                            //Get the Editable of each Module
                            Console.WriteLine("Module Editable: " + module.Editable);
                            //Get the EmailtemplateSupport of each Module
                            Console.WriteLine("Module EmailtemplateSupport: " + module.EmailtemplateSupport);
                            //Get the list of Profile instance each Module
                            List<MinifiedProfile> profiles = module.Profiles;
                            //Check if profiles is not null
                            if (profiles != null)
                            {
                                foreach (MinifiedProfile profile in profiles)
                                {
                                    //Get the Name of each Profile
                                    Console.WriteLine("Module Profile Name: " + profile.Name);
                                    //Get the Id of each Profile
                                    Console.WriteLine("Module Profile Id: " + profile.Id);
                                }
                            }
                            //Get the FilterSupported of each Module
                            Console.WriteLine("Module FilterSupported: " + module.FilterSupported);
                            //Get the ShowAsTab of each Module
                            Console.WriteLine("Module ShowAsTab: " + module.ShowAsTab);
                            //Get the WebLink of each Module
                            Console.WriteLine("Module WebLink: " + module.WebLink);
                            //Get the SequenceNumber of each Module
                            Console.WriteLine("Module SequenceNumber: " + module.SequenceNumber);
                            //Get the SingularLabel of each Module
                            Console.WriteLine("Module SingularLabel: " + module.SingularLabel);
                            //Get the Viewable of each Module
                            Console.WriteLine("Module Viewable: " + module.Viewable);
                            //Get the APISupported of each Module
                            Console.WriteLine("Module APISupported: " + module.APISupported);
                            //Get the APIName of each Module
                            Console.WriteLine("Module APIName: " + module.APIName);
                            //Get the QuickCreate of each Module
                            Console.WriteLine("Module QuickCreate: " + module.QuickCreate);
                            //Get the modifiedBy User instance of each Module
                            MinifiedUser modifiedBy = module.ModifiedBy;
                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the name of the modifiedBy User
                                Console.WriteLine("Module Modified By User-Name: " + modifiedBy.Name);
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Module Modified By User-ID: " + modifiedBy.Id);
                            }
                            //Get the GeneratedType of each Module
                            Console.WriteLine("Module GeneratedType: " + module.GeneratedType.Value);
                            //Get the FeedsRequired of each Module
                            Console.WriteLine("Module FeedsRequired: " + module.FeedsRequired);
                            //Get the ScoringSupported of each Module
                            Console.WriteLine("Module ScoringSupported: " + module.ScoringSupported);
                            //Get the WebformSupported of each Module
                            Console.WriteLine("Module WebformSupported: " + module.WebformSupported);
                            //Get the list of Argument instance each Module
                            List<Argument> arguments = module.Arguments;
                            //Check if arguments is not null
                            if (arguments != null)
                            {
                                foreach (Argument argument in arguments)
                                {
                                    //Get the Name of each Argument
                                    Console.WriteLine("Module Argument Name: " + argument.Name);
                                    //Get the Value of each Argument
                                    Console.WriteLine("Module Argument Value: " + argument.Value);
                                }
                            }
                            //Get the ModuleName of each Module
                            Console.WriteLine("Module ModuleName: " + module.ModuleName);
                            //Get the BusinessCardFieldLimit of each Module
                            Console.WriteLine("Module BusinessCardFieldLimit: " + module.BusinessCardFieldLimit);
                            //Get the parentModule Module instance of each Module
                            MinifiedModule parentModule = module.ParentModule;
                            //Check if arguments is not null
                            if (parentModule != null && parentModule.APIName != null)
                            {
                                //Get the Name of Parent Module
                                Console.WriteLine("Module Parent Module Name: " + parentModule.APIName);
                                //Get the Value of Parent Module
                                Console.WriteLine("Module Parent Module Id: " + parentModule.Id);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;
                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);
                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);
                        Console.WriteLine("Details: ");
                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }
                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message);
                    }
                }
                else
                { //If response is not as expected
                    //Get model object from response
                    Model responseObject = response.Model;
                    //Get the response object's class
                    Type type = responseObject.GetType();
                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    Console.WriteLine("Properties (N = {0}):", props.Length);
                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : ", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }
        private static void PrintCustomView(Com.Zoho.Crm.API.CustomViews.CustomViews customView)
        {
            //Get the DisplayValue of the CustomView
            Console.WriteLine("Module CustomView DisplayValue: " + customView.DisplayValue);
            //Get the SharedType of the CustomView
            Console.WriteLine("Module CustomView SharedType: " + customView.Category);
            //Get the SystemName of the CustomView
            Console.WriteLine("Module CustomView SystemName: " + customView.SystemName);
            // Get the Criteria instance of the CustomView
            Criteria criteria = customView.Criteria;
            //Check if criteria is not null
            if (criteria != null)
            {
                PrintCriteria(criteria);
            }
            //Get the list of SharedDetails instance of the CustomView
            List<SharedTo> sharedDetails = customView.SharedTo;
            if (sharedDetails != null)
            {
                foreach (SharedTo sharedDetail in sharedDetails)
                {
                    //Get the Name of the each SharedDetails
                    Console.WriteLine("Module SharedDetails Name: " + sharedDetail.Name);
                    //Get the ID of the each SharedDetails
                    Console.WriteLine("Module SharedDetails ID: " + sharedDetail.Id);
                    //Get the Type of the each SharedDetails
                    Console.WriteLine("Module SharedDetails Type: " + sharedDetail.Type);
                    //Get the Subordinates of the each SharedDetails
                    Console.WriteLine("Module SharedDetails Subordinates: " + sharedDetail.Subordinates);
                }
            }
            //Get the SortBy of the CustomView
            Console.WriteLine("Module CustomView SortBy: " + customView.SortBy);
            //Get the Offline of the CustomView
            Console.WriteLine("Module CustomView Offline: " + customView.Offline);
            //Get the Default of the CustomView
            Console.WriteLine("Module CustomView Default: " + customView.Default);
            //Get the SystemDefined of the CustomView
            Console.WriteLine("Module CustomView SystemDefined: " + customView.SystemDefined);
            //Get the Name of the CustomView
            Console.WriteLine("Module CustomView Name: " + customView.Name);
            //Get the ID of the CustomView
            Console.WriteLine("Module CustomView ID: " + customView.Id);
            //Get the Category of the CustomView
            Console.WriteLine("Module CustomView Category: " + customView.Category);
            //Get the list of string
            List<Fields> fields = customView.Fields;
            if (fields != null)
            {
                foreach (Fields field in fields)
                {
                    Console.WriteLine(field);
                }
            }
            if (customView.Favorite != null)
            {
                //Get the Favorite of the CustomView
                Console.WriteLine("Module CustomView Favorite: " + customView.Favorite);
            }
            if (customView.SortOrder != null)
            {
                //Get the SortOrder of the CustomView
                Console.WriteLine("Module CustomView SortOrder: " + customView.SortOrder);
            }
        }
        private static void PrintCriteria(Criteria criteria)
        {
            if (criteria.Comparator != null)
            {
                //Get the Comparator of the Criteria
                Console.WriteLine("Module CustomView Criteria Comparator: " + criteria.Comparator);
            }
            //Get the Field of the Criteria
            Console.WriteLine("Module CustomView Criteria Field: " + criteria.Field);
            if (criteria.Value != null)
            {
                //Get the Value of the Criteria
                Console.WriteLine("Module CustomView Criteria Value: " + criteria.Value);
            }
            // Get the List of Criteria instance of each Criteria
            List<Criteria> criteriaGroup = criteria.Group;
            if (criteriaGroup != null)
            {
                foreach (Criteria criteria1 in criteriaGroup)
                {
                    PrintCriteria(criteria1);
                }
            }
            //Get the Group Operator of the Criteria
            Console.WriteLine("Module CustomView Criteria Group Operator: " + criteria.GroupOperator);
        }
    }
}
