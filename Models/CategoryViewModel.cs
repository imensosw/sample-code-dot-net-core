using Memento.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Memento.Models
{
    public class CategoryViewModel
    {
        public List<TypeModel> TypeList { get; set; }
        public List<TypeCategoryModel> TypeCategoryList { get; set; }
        public List<CategoryModel> CategoryMList { get; set; }
        public CategoryModel category { get; set; }
        public string SelectedTypeName;
        public int SelectedTypeId;

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public CategoryViewModel CategoryList(int UserId,int TypeId,int CategoryId)
        {
            CategoryViewModel returnModel = new CategoryViewModel();
            returnModel.category = new CategoryModel();
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
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
            returnModel.SelectedTypeName = resultDT.Rows[0].Field<string>("TypeName");
            returnModel.SelectedTypeId= resultDT.Rows[0].Field<int>("TypeId");
            returnModel.CategoryMList = ObjectRelationMap.ConvertToList<CategoryModel>(resultDT);
            returnModel.category.TypeId = returnModel.SelectedTypeId;
            return returnModel;
        }
        public CategoryViewModel SelectCategoryList(int UserId, int CategoryId)
        {
            CategoryViewModel returnModel = new CategoryViewModel();
            returnModel.category = new CategoryModel();
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();
           using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SelectCategory";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);              
                cmd.Parameters.AddWithValue("Id", CategoryId);
                cnn.Open();

                //da.SelectCommand = cmd;
                //da.Fill(resultDT);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        Int32 Id = ((Int32)dr.GetValue(dr.GetOrdinal("Id")));
                        Int32 TypeId = ((Int32)dr.GetValue(dr.GetOrdinal("TypeId")));
                        String Name = ((String)dr.GetValue(dr.GetOrdinal("Name")));
                        String Description = ((String)dr.GetValue(dr.GetOrdinal("Description")));
                        String TypeName = ((String)dr.GetValue(dr.GetOrdinal("TypeName")));

                        returnModel.category.CategoryName = Name;
                        returnModel.category.Description = Description;
                        returnModel.category.TypeName = TypeName;
                        returnModel.category.CategoryId = Id;
                        returnModel.category.TypeId = TypeId;
                        returnModel.SelectedTypeName = TypeName;
                        

                    }
                }
              //  returnModel.CategoryMList = ObjectRelationMap.ConvertToList<CategoryModel>(resultDT);
            }
            
            return returnModel;
        }
        public CategoryViewModel TypeMasterList(int UserId)
        {
            CategoryViewModel returnModel = new CategoryViewModel();
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_TypeMasterList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);
                cnn.Open();
                da.SelectCommand = cmd;
                da.Fill(resultDT);
            }
            returnModel.TypeList = ObjectRelationMap.ConvertToList<TypeModel>(resultDT);
            return returnModel;
        }
        public CategoryViewModel TypeCategoryCount(int UserId)
        {
            CategoryViewModel returnModel = new CategoryViewModel();
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_TypeCategoryList";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("UserID", UserId);                
                cnn.Open();
                da.SelectCommand = cmd;
                da.Fill(resultDT);
            }
            returnModel.TypeCategoryList = ObjectRelationMap.ConvertToList<TypeCategoryModel>(resultDT);
           
            return returnModel;
        }
        public CategoryViewModel SaveCategory(CategoryViewModel returnModel, int UserId)
        {           
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveCategory";
                //------add any parameters the stored procedure might require------------------//
                cmd.Parameters.AddWithValue("Id", returnModel.category.CategoryId);
                cmd.Parameters.AddWithValue("Name", returnModel.category.CategoryName);
                cmd.Parameters.AddWithValue("TypeId", returnModel.category.TypeId);               
                cmd.Parameters.AddWithValue("UserID", UserId);
                
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
           

            return returnModel;
        }
        public CategoryViewModel DeleteCategory(int Id, int UserId)
        {
            CategoryViewModel returnModel = new CategoryViewModel();
            DataTable resultDT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            UserId = 1;
            string cnnString = CommonHelper.GetConnectionString();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleteCategory";
                //------add any parameters the stored procedure might require------------------//
                
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.Parameters.AddWithValue("UserID", UserId);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }


            return returnModel;
        }
    }
}

