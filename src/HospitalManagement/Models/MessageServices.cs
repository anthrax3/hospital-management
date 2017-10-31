using HospitalManagement.ViewModels.Account;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class MessageServices
    {
        public async static Task SendEmail( SignUpViewModel model, ApplicationDbContext _context)
        {

            ConfirmUser user = new ConfirmUser();
            user.About = model.About;
            user.Address = model.Address;
            user.City = model.City;
            user.Contact = model.ContactNo;
            user.Country = model.Country;
            user.DOB = model.Birthday;
            user.Email = model.Email;
            user.Fname = model.Fname;
            user.Gender = model.Gender;
            user.Lname = model.Lname;
            user.Password = model.LoginPassword;
            user.SignUpAs = model.Type;

            string randomToken = Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("+", string.Empty).Replace("=", string.Empty).Replace("/", string.Empty).Substring(0, 4);
            user.Token = randomToken;
            
            _context.ConfirmUser.Add(user);
            _context.SaveChanges();
            

            try
            {

                string message = "Please confirm your account by clicking this link: <br /> <a href='http://localhost:54118/Accounts/UserLogin/?Token=" + randomToken +"&Email="+model.Email+"'> Click this link</a>";
                string subject = "Account Confirmation Email from Doctor's Hospital";

                var _email = "drhospital@hotmail.com";
                var _password = "Cigf6789";
                var _dispName = "Doctor's Hospital";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(model.Email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;




                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                   await smtp.SendMailAsync(myMessage);

                }
               
                

        }
            catch (Exception e)
            {
                throw e;
            }



        }


    }
}
