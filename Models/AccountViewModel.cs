using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Memento.Helper;
using Memento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Data.SqlClient;


namespace Memento.Models
{
    public class AccountViewModel
    {
      //  public AccountModel accountModel { get; set; }


        #region MODELS              
        //----------------------REGION------------------------------------//


        public LoginModel loginModel { get; set; }


        //----------------------END OF REGION----------------------------//
        #endregion



        #region PUBLIC_FUNCTIONS
        //----------------------REGION------------------------------------//

       
        public LoginModel Login(LoginModel loginModel, String subDomain)
        {
            try
            {
               // var ePassword1 = CommonHelper.Encrypt_Password_MD5(loginModel.Password);
                var ePassword = CommonHelper.EncryptText(loginModel.Password);
                
                string IPAddress = SessionHelper.GetIPAddress();

                string cnnString = CommonHelper.GetConnectionString();
                
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_login";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("SubDomain", subDomain);
                    cmd.Parameters.AddWithValue("EmailAddress", loginModel.EmailAddress);
                    cmd.Parameters.AddWithValue("Password", ePassword);
                    cmd.Parameters.Add("@ErrorCode", SqlDbType.VarChar,20);
                    cmd.Parameters["@ErrorCode"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar,150);
                    cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            Int32 accountID = ((Int32)dr.GetValue(dr.GetOrdinal("UserId")));
                            string errorCode = ((String)dr.GetValue(dr.GetOrdinal("ErrorCode")));
                            string errorMessage = ((String)dr.GetValue(dr.GetOrdinal("ErrorMessage")));
                            string FirstName = ((String)dr.GetValue(dr.GetOrdinal("FirstName")));
                            string LastName = ((String)dr.GetValue(dr.GetOrdinal("LastName")));
                            string UserType = ((String)dr.GetValue(dr.GetOrdinal("UserType")));
                            // string fullApplicationPath = ((string)dr.GetValue(dr.GetOrdinal("FullApplicationPath")));

                            if (accountID > 0)
                            {   //loginModel.PaymentCode = result.PaymentCode;
                                loginModel.UserName = loginModel.EmailAddress;
                                loginModel.UserId = accountID;
                                //loginModel.FullApplicationPath = fullApplicationPath;
                                loginModel.userSession = new UserSession();
                                //loginModel.userSession.PaymentCode = result.PaymentCode;
                                var nodes = loginModel.EmailAddress.Split('@');

                                loginModel.userSession.UserName = FirstName+' '+LastName;
                                loginModel.userSession.EmailAddress = loginModel.EmailAddress;
                                loginModel.userSession.UserID = accountID;
                               loginModel.userSession.SubDomainName = subDomain;
                                loginModel.userSession.UserType = UserType;

                            }
                            loginModel.ErrorCode = errorCode;
                            loginModel.ErrorMessage = errorMessage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               // DatabaseHelper.LogErrorToDatabase(ex, "", "Account/Login");
                loginModel.ErrorCode = "Error";
                loginModel.ErrorMessage = "There was a problem executing this action.";
            }
            return loginModel;
        }




        public LoginModel VerifyRegistrationEmail(String pa, String pc, String vc, String subDomain)
        {
            try
            {
                LoginModel loginModel = new LoginModel();
                string IPAddress = SessionHelper.GetIPAddress();
                string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalConn"].ConnectionString;

                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Portal_VerifyAccount";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("SubDomain", subDomain);
                    cmd.Parameters.AddWithValue("PortalAccountID", pa);
                    cmd.Parameters.AddWithValue("PaymentCode", pc);
                    cmd.Parameters.AddWithValue("VerificationCode", vc);
                    //cmd.Parameters.AddWithValue("IPAddress", IPAddress);
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            //Int32 accountID = ((Int32)dr.GetValue(dr.GetOrdinal("PortalAccountID")));
                            String errorCode = ((String)dr.GetValue(dr.GetOrdinal("ErrorCode")));
                            String errorMessage = ((String)dr.GetValue(dr.GetOrdinal("errorMessage")));

                            if (errorCode == "Error")
                            {
                                loginModel.ErrorCode = errorCode;
                                loginModel.ErrorMessage = errorMessage;
                            }
                        }
                    }
                    else
                    {
                        loginModel.ErrorCode = "Error";
                        loginModel.ErrorMessage = "Account Not Found.";
                    }
                }
                return loginModel;
            }
            catch (Exception ex)
            {
                DatabaseHelper.LogErrorToDatabase(ex, "", "Account/VerifyRegistrationEmail");
                return loginModel;
            }
        }

        public LoginModel IsVerifyEmail(int id, String subDomain)
        {
            try
            {
                LoginModel loginModel = new LoginModel();
                string IPAddress = SessionHelper.GetIPAddress();
                string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalConn"].ConnectionString;

                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Portal_VerifyAccountOLD";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("SubDomain", subDomain);
                    cmd.Parameters.AddWithValue("PortalAccountID", id);
                    //cmd.Parameters.AddWithValue("IPAddress", IPAddress);
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            //Int32 accountID = ((Int32)dr.GetValue(dr.GetOrdinal("PortalAccountID")));
                            String errorCode = ((String)dr.GetValue(dr.GetOrdinal("ErrorCode")));
                            String errorMessage = ((String)dr.GetValue(dr.GetOrdinal("errorMessage")));

                            if (errorCode == "Error")
                            {
                                loginModel.ErrorCode = errorCode;
                                loginModel.ErrorMessage = errorMessage;
                            }
                        }
                    }
                    else
                    {

                        loginModel.ErrorCode = "Error";
                        loginModel.ErrorMessage = "Account Not Found.";

                    }

                }

                return loginModel;
            }
            catch (Exception ex)
            {
                DatabaseHelper.LogErrorToDatabase(ex, "", "Account/IsVerifyEmail");
                return loginModel;
            }

        }


        public LoginModel PasswordChange(LoginModel model)
        {
            try
            {
                //string newPassword = CommonHelper.Encrypt_Password_MD5(model.NewPassword);
                //string oldPassword = CommonHelper.Encrypt_Password_MD5(model.oldPassword);
                string newPassword = CommonHelper.EncryptText(model.NewPassword);
                string oldPassword = CommonHelper.EncryptText(model.oldPassword);

                var UserId = SessionHelper.GetCurrentUserID();
                var Emailaddress = SessionHelper.GetCurrentEmailaddress();
                //var vPaymentCodeID = db.usp_Portal_PasswordChange(UserId, Emailaddress, oldPassword, newPassword, oUserid).ToList();

                string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalConn"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Portal_UpdatePassword";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("PortalAccountID", UserId);
                    cmd.Parameters.AddWithValue("EmailAddress", Emailaddress);
                    cmd.Parameters.AddWithValue("OldPassword", oldPassword);
                    cmd.Parameters.AddWithValue("NewPassword", newPassword);
                    //cmd.Parameters.AddWithValue("IPAddress", IPAddress);
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            Int32 accountID = ((Int32)dr.GetValue(dr.GetOrdinal("PortalAccountID")));
                            String errorCode = ((String)dr.GetValue(dr.GetOrdinal("ErrorCode")));
                            String errorMessage = ((String)dr.GetValue(dr.GetOrdinal("errorMessage")));

                            loginModel.ErrorCode = errorCode;
                            loginModel.ErrorMessage = errorMessage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DatabaseHelper.LogErrorToDatabase(ex, "", "Account/PasswordChange");
                model.ErrorCode = "Error";
                model.ErrorMessage = "There was a problem executing this action.";
            }
            return model;
        }


        public LoginModel ForgotPassword(LoginModel model, String subDomain)
        {
            try
            {
                string newPassword = CommonHelper.GeneratePassword();
                //var ePassword = CommonHelper.Encrypt_Password_MD5(newPassword);
                var ePassword = CommonHelper.EncryptText(newPassword);

                string IPAddress = SessionHelper.GetIPAddress();
                string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalConn"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Portal_ResetPassword";
                    //------add any parameters the stored procedure might require------------------//
                    cmd.Parameters.AddWithValue("SubDomain", subDomain);
                    cmd.Parameters.AddWithValue("EmailAddress", loginModel.EmailAddress);
                    cmd.Parameters.AddWithValue("Password", ePassword);
                    //cmd.Parameters.AddWithValue("IPAddress", IPAddress);
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            Int32 accountID = ((Int32)dr.GetValue(dr.GetOrdinal("PortalAccountID")));
                            String errorCode = ((String)dr.GetValue(dr.GetOrdinal("ErrorCode")));
                            String errorMessage = ((String)dr.GetValue(dr.GetOrdinal("errorMessage")));

                            loginModel.ErrorCode = errorCode;
                            loginModel.ErrorMessage = errorMessage;

                            // proceed with attempting to send the email if there was an account/success from procedure
                            if (errorCode == "success")
                            {
                                if (SendEmail(loginModel.EmailAddress, newPassword) == false)
                                {
                                    loginModel.ErrorCode = "Error";
                                    loginModel.ErrorMessage = "There was a problem sending the email!";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DatabaseHelper.LogErrorToDatabase(ex, "", "Account/ForgotPassword");
                loginModel.ErrorCode = "Error";
                loginModel.ErrorMessage = "There was a problem executing this action.";
            }
            return loginModel;
        }



       





        //----------------------END OF REGION----------------------------//
        #endregion



        #region PRIVATE_FUNCTIONS
        //----------------------REGION------------------------------------//

        private static Boolean SendVerificationLinkEmail(String emailAddress, Int32 portalAccountID, String paymentCode, String verificationCode, String Url, String password)
        {
            var verifyUrl = Url + "?pa=" + portalAccountID.ToString() + "&pc=" + paymentCode + "&vc=" + verificationCode;
            //'-------------------------------------------------------------------------------------'
            SMTPSettingsModel smtpSettings = DatabaseHelper.GetSMTPSettings();
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(smtpSettings.Host);
            smtpClient.Port = smtpSettings.Port;
            smtpClient.EnableSsl = smtpSettings.SSLRequired;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;

            if (smtpSettings.RequiresAuthentication == true)
            {
                smtpClient.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
            }

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new System.Net.Mail.MailAddress(smtpSettings.EmailFrom, "Patient Bill Support");
            mailMessage.To.Add(new System.Net.Mail.MailAddress(emailAddress));
            mailMessage.Bcc.Add(new System.Net.Mail.MailAddress(smtpSettings.EmailFrom, "Patient Bill Support"));
            mailMessage.Subject = "Your PRC account activation link";
            mailMessage.Body = "<br/><br/>Thank you for registering.<br/><br/>Your account has been successfully created for the Patient Billing Portal. Please verify and login using the verification link and password provided below: " +
                " <br/><br/> <a href='" + verifyUrl + "'>Verification Link</a>" + "<br/><br/> Your password : " + password + "<br/><br/><br/><b>Note: </b>The Verification link will expire in 24 hours. " + "<br/><br/><br/><br/> Thank You.<br/>Patient Billing Portal Team";
            bool sentSuccessfully = false;

            //--- Try to send the email and handle any errors on send ----//
            try
            {
                smtpClient.Send(mailMessage);
                sentSuccessfully = true;
            }
            catch (Exception ex)
            {
                DatabaseHelper.LogErrorToDatabase(ex, "", "Account/SendVerificationLinkEmail");
                sentSuccessfully = false;
            }

            //' ------------------------------------------------------------------------------------'           
            return sentSuccessfully;
        }


        private static Boolean SendEmail(string emailAddress, string Password)
        {
            //---Forgot Password function ----///
            //'-------------------------------------------------------------------------------------'
            SMTPSettingsModel smtpSettings = DatabaseHelper.GetSMTPSettings();
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(smtpSettings.Host);
            smtpClient.Port = smtpSettings.Port;
            smtpClient.EnableSsl = smtpSettings.SSLRequired;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;

            if (smtpSettings.RequiresAuthentication == true)
            {
                smtpClient.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
            }

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new System.Net.Mail.MailAddress(smtpSettings.EmailFrom, "Patient Bill Support");
            mailMessage.To.Add(new System.Net.Mail.MailAddress(emailAddress));
            // mailMessage.Bcc.Add("");    ------ NOTE:  DO NOT BCC WHEN SENDING PASSWORD OUT.  THIS IS HIPAA DATA-------
            mailMessage.Subject = "Your PatientBillHelp password has been reset";
            mailMessage.Body = "Hi there,<br/><br/>You are receiving this email because we reset your password. <br/><br/>Use the below password to login." + "<br/><br/> New Password : " + Password + "<br/><br/><br/><br/><br/><br/> Best Regards,<br/><br/>PRC";
            bool sentSuccessfully = false;

            //--- Try to send the email and handle any errors on send ----//
            try
            {
                smtpClient.Send(mailMessage);
                sentSuccessfully = true;
            }
            catch (Exception ex)
            {
                DatabaseHelper.LogErrorToDatabase(ex, "", "Account/SendEmail");
                sentSuccessfully = false;
            }

            //' ------------------------------------------------------------------------------------'                      
            return sentSuccessfully;
        }



        //----------------------END OF REGION----------------------------//
        #endregion
    }
}
