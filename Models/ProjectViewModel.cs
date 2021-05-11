using Memento.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Memento.Models
{
    public class ProjectViewModel
    {
        public List<CategoryModel> CategoryMList { get; set; }
        public List<ProjectModel> ProjectMList { get; set; }
        public ProjectModel ProjectM { get; set; }

        public ProjectViewModel CategoryTypeList(int UserId)
        {
            ProjectViewModel returnModel = new ProjectViewModel();
            DataTable resultDT = new DataTable();
            returnModel.ProjectM = new ProjectModel();
            returnModel.ProjectM.IndustriesList = new List<IndustriesList>();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            int TypeId = 0;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_CategoryList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("TypeID", TypeId);
                cnn.Open();
                da.SelectCommand = cmd;
                da.Fill(resultDT);
            }

            if (resultDT.Rows.Count > 0)
            {
                DataTable Industryresult = new DataTable();
                DataTable Servicesresult = new DataTable();
                DataTable Technologiesresult = new DataTable();
                DataTable Tagsresult = new DataTable(); 

                var industrytmp = from d in resultDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Industry")
                                  select d;
                if (industrytmp.Count() > 0)
                {
                    Industryresult = industrytmp.CopyToDataTable();
                }
                //DataTable Industryresult = (from d in resultDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Industry")
                //                            select d).CopyToDataTable();

                var servicetmp = from d in resultDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Services")
                                 select d;
                if (servicetmp.Count() > 0)
                {
                    Servicesresult = servicetmp.CopyToDataTable();
                }
                var technologytmp = from d in resultDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Technologies")
                                    select d;
                if (technologytmp.Count() > 0)
                {
                    Technologiesresult = technologytmp.CopyToDataTable();
                }
                var tagtmp = from d in resultDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Tags")
                             select d;
                if (tagtmp.Count() > 0)
                {
                    Tagsresult = tagtmp.CopyToDataTable();
                }
                returnModel.ProjectM.IndustriesList = ObjectRelationMap.ConvertToList<IndustriesList>(Industryresult);
                returnModel.ProjectM.ServicesList = ObjectRelationMap.ConvertToList<ServicesList>(Servicesresult);
                returnModel.ProjectM.TechnologiesList = ObjectRelationMap.ConvertToList<TechnologiesList>(Technologiesresult);
                returnModel.ProjectM.TagsList = ObjectRelationMap.ConvertToList<TagsList>(Tagsresult);

            }
            return returnModel;
        }

        
        public ProjectViewModel ProjectList(int UserId,int Id)
        {
            ProjectViewModel returnModel = new ProjectViewModel();
            DataTable resultDT = new DataTable();
            DataTable resultCategoryDT = new DataTable();
            returnModel.ProjectM = new ProjectModel();
            returnModel.ProjectM.IndustriesList = new List<IndustriesList>();
            returnModel.ProjectM.ServicesList = new List<ServicesList>();
            returnModel.ProjectM.TechnologiesList = new List<TechnologiesList>();
            returnModel.ProjectM.TagsList = new List<TagsList>();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ProjectList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("ProjectId", Id);
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
                da.SelectCommand = cmd;
                da.Fill(resultDT);
            }
            returnModel.ProjectMList = ObjectRelationMap.ConvertToList<ProjectModel>(resultDT);
            
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ProjectCategoryList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("ProjectId", Id);
                cmd.Parameters.AddWithValue("SearchId", "");
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(resultCategoryDT);
            }

            if (resultCategoryDT.Rows.Count > 0)
            {
                DataTable Industryresult = new DataTable();
                DataTable Servicesresult = new DataTable();
                DataTable Technologiesresult = new DataTable();
                DataTable Tagsresult = new DataTable();
              
                var industrytmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Industry")
                                   select d).Distinct();
                if (industrytmp.Count() > 0)
                {
                    Industryresult = industrytmp.CopyToDataTable();
                }

                var servicetmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Services")
                                  select d).Distinct();
                if (servicetmp.Count() > 0)
                {
                    Servicesresult = servicetmp.CopyToDataTable();
                }
                var technologytmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Technologies")
                                     select d).Distinct();
                if (technologytmp.Count() > 0)
                {
                    Technologiesresult = technologytmp.CopyToDataTable();
                }
                var tagtmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Tags")
                              select d).Distinct();
                if (tagtmp.Count() > 0)
                {
                    Tagsresult = tagtmp.CopyToDataTable();
                }

                returnModel.ProjectM.IndustriesList = ObjectRelationMap.ConvertToList<IndustriesList>(Industryresult);
                returnModel.ProjectM.ServicesList = ObjectRelationMap.ConvertToList<ServicesList>(Servicesresult);
                returnModel.ProjectM.TechnologiesList = ObjectRelationMap.ConvertToList<TechnologiesList>(Technologiesresult);
                returnModel.ProjectM.TagsList = ObjectRelationMap.ConvertToList<TagsList>(Tagsresult);
            }
            return returnModel;
        }

        public ProjectViewModel EditProject(int UserId, int Id)
        {
            ProjectViewModel returnModel = new ProjectViewModel();
            DataTable resultDT = new DataTable();
            DataTable resultCategoryDT = new DataTable();
            returnModel.ProjectM = new ProjectModel();
            returnModel.ProjectM.IndustriesList = new List<IndustriesList>();
            returnModel.ProjectM.ServicesList = new List<ServicesList>();
            returnModel.ProjectM.TechnologiesList = new List<TechnologiesList>();
            returnModel.ProjectM.TagsList = new List<TagsList>();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_EditProjectList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("ProjectId", Id);
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
                da.SelectCommand = cmd;
                da.Fill(resultDT);
            }
            returnModel.ProjectMList = ObjectRelationMap.ConvertToList<ProjectModel>(resultDT);
            if(resultDT != null && resultDT.Rows.Count > 0)
            {
                for(int i = 0; i < resultDT.Rows.Count; i++)
                {
                    returnModel.ProjectM.Id =Convert.ToInt32(resultDT.Rows[i]["Id"].ToString());
                    returnModel.ProjectM.Title = resultDT.Rows[i]["Title"].ToString();
                    returnModel.ProjectM.Description = resultDT.Rows[i]["Description"].ToString();
                    returnModel.ProjectM.ProjectImagePath = resultDT.Rows[i]["ProjectImagePath"].ToString();

                    returnModel.ProjectM.DemoDetail = resultDT.Rows[i]["DemoDetail"].ToString();
                    returnModel.ProjectM.FAuthorName = resultDT.Rows[i]["FAuthorName"].ToString();
                    returnModel.ProjectM.FAuthorImagePath = resultDT.Rows[i]["FAuthorImagePath"].ToString();
                    returnModel.ProjectM.ClientName = resultDT.Rows[i]["ClientName"].ToString();
                    returnModel.ProjectM.ClientLogoPath = resultDT.Rows[i]["ClientLogoPath"].ToString();
                    returnModel.ProjectM.RUDDocumentPath = resultDT.Rows[i]["RUDDocumentPath"].ToString();
                    returnModel.ProjectM.InitialScopeDocPath = resultDT.Rows[i]["InitialScopeDocPath"].ToString();
                    returnModel.ProjectM.UpdatedScopeDocPath = resultDT.Rows[i]["UpdatedScopeDocPath"].ToString();
                    returnModel.ProjectM.FFeedback = resultDT.Rows[i]["FFeedback"].ToString();

                    
                    //var arr=resultDT.Rows[i]["IndustryCategoryId"].ToString().Split(",").Select(x=>Convert.ToInt32(x)).ToList();                  
                    //returnModel.ProjectM.IndustriesSelectedList = arr;

                }
            }

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ProjectCategoryList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("ProjectId",0);
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(resultCategoryDT);
            }

            if (resultCategoryDT.Rows.Count > 0)
            {
                DataTable Industryresult = new DataTable();
                DataTable Servicesresult = new DataTable();
                DataTable Technologiesresult = new DataTable();
                DataTable Tagsresult = new DataTable();

                var industrytmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Industry")
                                   select d).Distinct();
                if (industrytmp.Count() > 0)
                {
                    Industryresult = industrytmp.CopyToDataTable();
                }

                var servicetmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Services")
                                  select d).Distinct();
                if (servicetmp.Count() > 0)
                {
                    Servicesresult = servicetmp.CopyToDataTable();
                }
                var technologytmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Technologies")
                                     select d).Distinct();
                if (technologytmp.Count() > 0)
                {
                    Technologiesresult = technologytmp.CopyToDataTable();
                }
                var tagtmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Tags")
                              select d).Distinct();
                if (tagtmp.Count() > 0)
                {
                    Tagsresult = tagtmp.CopyToDataTable();
                }

                returnModel.ProjectM.IndustriesList = ObjectRelationMap.ConvertToList<IndustriesList>(Industryresult);
                returnModel.ProjectM.ServicesList = ObjectRelationMap.ConvertToList<ServicesList>(Servicesresult);
                returnModel.ProjectM.TechnologiesList = ObjectRelationMap.ConvertToList<TechnologiesList>(Technologiesresult);
                returnModel.ProjectM.TagsList = ObjectRelationMap.ConvertToList<TagsList>(Tagsresult);

               if (resultDT.Rows[0]["IndustryCategoryId"].ToString().Length > 0)
                {
                    var indusarr = resultDT.Rows[0]["IndustryCategoryId"].ToString().Split(",").Select(x => Convert.ToInt32(x)).ToList();
                    returnModel.ProjectM.IndustriesSelectedList = indusarr;
                }
                if (resultDT.Rows[0]["ServicesCategoryId"].ToString().Length > 0)
                {
                    var serarr = resultDT.Rows[0]["ServicesCategoryId"].ToString().Split(",").Select(x => Convert.ToInt32(x)).ToList();
                    returnModel.ProjectM.ServicesSelectedList = serarr;
                }
                if (resultDT.Rows[0]["TechnologiesCategoryId"].ToString().Length > 0)
                {
                    var techarr = resultDT.Rows[0]["TechnologiesCategoryId"].ToString().Split(",").Select(x => Convert.ToInt32(x)).ToList();
                    returnModel.ProjectM.TechnologiesSelectedList = techarr;
                }
                if (resultDT.Rows[0]["TagsCategoryId"].ToString().Length > 0)
                {
                    var tagarr = resultDT.Rows[0]["TagsCategoryId"].ToString().Split(",").Select(x => Convert.ToInt32(x)).ToList();
                    returnModel.ProjectM.TagsSelectedList = tagarr;
                }
            }
            return returnModel;
        }

        public ProjectViewModel SaveProject(ProjectViewModel model, int UserId)
        {
            int ProjectId = 0;
            string Mode = "";
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            if(model.ProjectM.Id>0)
            {
                Mode = "Edit";
            }
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveProject";
                //------add any parameters the stored procedure might require------------------//
                
                cmd.Parameters.AddWithValue("Id", model.ProjectM.Id);
                cmd.Parameters.AddWithValue("Title", model.ProjectM.Title);
                cmd.Parameters.AddWithValue("Description", model.ProjectM.Description);
                cmd.Parameters.AddWithValue("ClientName", model.ProjectM.ClientName);
                cmd.Parameters.AddWithValue("ClientLogoPath", model.ProjectM.ClientLogoPath);
                cmd.Parameters.AddWithValue("RUDDocumentPath", model.ProjectM.RUDDocumentPath);
                cmd.Parameters.AddWithValue("InitialScopeDocPath", model.ProjectM.InitialScopeDocPath);
                cmd.Parameters.AddWithValue("UpdatedScopeDocPath", model.ProjectM.UpdatedScopeDocPath);
                cmd.Parameters.AddWithValue("ProjectImagePath", model.ProjectM.ProjectImagePath);
                cmd.Parameters.AddWithValue("DemoDetail", model.ProjectM.DemoDetail);

                cmd.Parameters.AddWithValue("FAuthorName", model.ProjectM.FAuthorName);
                cmd.Parameters.AddWithValue("FAuthorImagePath", model.ProjectM.FAuthorImagePath);
                cmd.Parameters.AddWithValue("FFeedback", model.ProjectM.FFeedback);
                cmd.Parameters.AddWithValue("Status", "Active");
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.Add("@ProjectId",SqlDbType.Int);
                cmd.Parameters["@ProjectId"].Direction = ParameterDirection.Output;               
                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    //Storing the output parameters value in 3 different variables.  
                    ProjectId = Convert.ToInt32(cmd.Parameters["@ProjectId"].Value);                    
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    cnn.Close();
                }
            }
            if (ProjectId > 0)
            {
                if (model.ProjectM.IndustriesSelectedList!=null && model.ProjectM.IndustriesSelectedList.Count() > 0)
                {
                    DeleteProjectCategory(ProjectId, "Industry");
                    SaveProjectCategory(ProjectId, UserId, "Industry", model.ProjectM.IndustriesSelectedList);
                }
                if(model.ProjectM.ServicesSelectedList != null && model.ProjectM.ServicesSelectedList.Count() > 0)
                {
                    DeleteProjectCategory(ProjectId, "Services");
                    SaveProjectCategory(ProjectId, UserId, "Services", model.ProjectM.ServicesSelectedList);
                }
                if (model.ProjectM.TechnologiesSelectedList != null && model.ProjectM.TechnologiesSelectedList.Count() > 0)
                {
                    DeleteProjectCategory(ProjectId, "Technologies");
                    SaveProjectCategory(ProjectId, UserId, "Technologies", model.ProjectM.TechnologiesSelectedList);
                }
                 if (model.ProjectM.TagsSelectedList != null && model.ProjectM.TagsSelectedList.Count() > 0)
                {
                    DeleteProjectCategory(ProjectId, "Tags");
                    SaveProjectCategory(ProjectId, UserId, "Tags", model.ProjectM.TagsSelectedList);
                }

            }
            return model;
        }
        public void SaveProjectCategory(int ProjectId,int UserId,string CategoryName, List<int> CategoryList)
        {
            int ErrorId = 0;
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                for (int i = 0; i <= CategoryList.Count()-1;i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_SaveProjectCategory";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("ProjectId", ProjectId);                     
                    cmd.Parameters.AddWithValue("TypeName", CategoryName);
                    cmd.Parameters.AddWithValue("CategoryId", CategoryList[i]);
                    cmd.Parameters.AddWithValue("UserID", UserId);                   
                    cmd.Parameters.Add("@ErrorId", SqlDbType.Int);
                    cmd.Parameters["@ErrorId"].Direction = ParameterDirection.Output;
                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        //Storing the output parameters value in 3 different variables.  
                        ErrorId = Convert.ToInt32(cmd.Parameters["@ErrorId"].Value);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    finally
                    {
                        cnn.Close();
                    }                   
                }                
            }
        }
        public void DeleteProjectCategory(int ProjectId, string CategoryName)
        {                           
            string cnnString = CommonHelper.GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {                
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_DeleteProjectCategory";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("Id", ProjectId);                   
                    cmd.Parameters.AddWithValue("TypeName", CategoryName);                  
                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                       
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    finally
                    {
                        cnn.Close();
                    }
              
            }
        }

        public ProjectViewModel DeleteProject(int Id, int UserId)
        {
            ProjectViewModel returnModel = new ProjectViewModel();
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleteProject";
                //------add any parameters the stored procedure might require------------------//

                cmd.Parameters.AddWithValue("Id", Id);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }


            return returnModel;
        }

        public ProjectViewModel SearchProject(ProjectViewModel returnModel)
        {
            string industrySearchstring = "";
            string serviceSearchstring = "";
            string technologySearchstring = "";
            string tagSearchstring = "";

            if (returnModel.ProjectM.IndustryId > 0)
            {
                if (industrySearchstring.Length<=0)
                {
                    industrySearchstring = returnModel.ProjectM.IndustryId.ToString();
                }
                else
                {
                    industrySearchstring =  returnModel.ProjectM.IndustryId.ToString();
                }

            }
            if(returnModel.ProjectM.ServiceId > 0)
            {
                if (serviceSearchstring.Length <= 0)
                {
                    serviceSearchstring = returnModel.ProjectM.ServiceId.ToString();
                }
                else
                {
                    serviceSearchstring =  returnModel.ProjectM.ServiceId.ToString();
                }

            }
            if (returnModel.ProjectM.TechnologyId > 0)
            {
                if (technologySearchstring.Length <= 0)
                {
                    technologySearchstring = returnModel.ProjectM.TechnologyId.ToString();
                }
                else
                {
                    technologySearchstring =  returnModel.ProjectM.TechnologyId.ToString();
                }
            }
            if (returnModel.ProjectM.tagId > 0)
            {
                if (tagSearchstring.Length <= 0)
                {
                    tagSearchstring = returnModel.ProjectM.tagId.ToString();
                }
                else
                {
                    tagSearchstring = returnModel.ProjectM.tagId.ToString();
                }
            }


            DataTable projectresult = new DataTable();
            DataTable resultDT = new DataTable();
            DataTable resultCategoryDT = new DataTable();
            returnModel.ProjectM = new ProjectModel();
            returnModel.ProjectM.IndustriesList = new List<IndustriesList>();
            returnModel.ProjectM.ServicesList = new List<ServicesList>();
            returnModel.ProjectM.TechnologiesList = new List<TechnologiesList>();
            returnModel.ProjectM.TagsList = new List<TagsList>();
            SqlDataAdapter da = new SqlDataAdapter();
            int UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ProjectList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("ProjectId", 0);
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
                da.SelectCommand = cmd;
                da.Fill(resultDT);
            }
            var vprojectresult = (from d in resultDT.AsEnumerable().Where(m => m.Field<string>("Status").Trim() == "Active") select d).Distinct();
            if (vprojectresult.Count()> 0)
            {
                if (industrySearchstring.Length > 0)
                {
                    vprojectresult = (from d in vprojectresult.AsEnumerable().Where(m => m.Field<string>("IndustryCategoryId").Contains(industrySearchstring))
                                      select d).Distinct();
                }
                                    
              if (serviceSearchstring.Length>0)
                {
                     vprojectresult = (from d in vprojectresult.AsEnumerable().Where(m =>  m.Field<string>("ServicesCategoryId").Contains(serviceSearchstring))
                                          select d).Distinct();
                }
                if (technologySearchstring.Length > 0)
                {
                    vprojectresult = (from d in vprojectresult.AsEnumerable().Where(m => m.Field<string>("TechnologiesCategoryId").Contains(technologySearchstring))
                                      select d).Distinct();
                }
                if (tagSearchstring.Length > 0)
                {
                    vprojectresult = (from d in vprojectresult.AsEnumerable().Where(m => m.Field<string>("TagsCategoryId").Contains(tagSearchstring))
                                      select d).Distinct();
                }
                if (vprojectresult.Count() > 0)
                {
                    projectresult = vprojectresult.CopyToDataTable();
                }
            }
            if (projectresult.Rows.Count > 0)
            {
                returnModel.ProjectMList = ObjectRelationMap.ConvertToList<ProjectModel>(projectresult);
            }
            

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ProjectCategoryList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cmd.Parameters.AddWithValue("ProjectId", 0);
                cmd.Parameters.AddWithValue("SearchId", "");
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(resultCategoryDT);
            }

            if (resultCategoryDT.Rows.Count > 0)
            {
                DataTable Industryresult = new DataTable();
                DataTable Servicesresult = new DataTable();
                DataTable Technologiesresult = new DataTable();
                DataTable Tagsresult = new DataTable();

                var industrytmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Industry")
                                   select d).Distinct();
                if (industrytmp.Count() > 0)
                {
                    Industryresult = industrytmp.CopyToDataTable();
                }

                var servicetmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Services")
                                  select d).Distinct();
                if (servicetmp.Count() > 0)
                {
                    Servicesresult = servicetmp.CopyToDataTable();
                }
                var technologytmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Technologies")
                                     select d).Distinct();
                if (technologytmp.Count() > 0)
                {
                    Technologiesresult = technologytmp.CopyToDataTable();
                }
                var tagtmp = (from d in resultCategoryDT.AsEnumerable().Where(m => m.Field<String>("TypeName") == "Tags")
                              select d).Distinct();
                if (tagtmp.Count() > 0)
                {
                    Tagsresult = tagtmp.CopyToDataTable();
                }

                returnModel.ProjectM.IndustriesList = ObjectRelationMap.ConvertToList<IndustriesList>(Industryresult);
                returnModel.ProjectM.ServicesList = ObjectRelationMap.ConvertToList<ServicesList>(Servicesresult);
                returnModel.ProjectM.TechnologiesList = ObjectRelationMap.ConvertToList<TechnologiesList>(Technologiesresult);
                returnModel.ProjectM.TagsList = ObjectRelationMap.ConvertToList<TagsList>(Tagsresult);
            }
            return returnModel;
        }
    }
}

