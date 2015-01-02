using ADFSAndWIFCoded.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;

namespace ADFSAndWIFCoded.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginUser(User user)
        {
            switch (user.SelectedAuthenticationOption)
            {
                case AuthenticationOptions.Standard:
                    return LoginUserStandard(user);
                case AuthenticationOptions.ActiveDirectory:
                    return LoginUserActiveDirectory(user);
                case AuthenticationOptions.ADFSAndWif:
                    return View("Index");
                default:
                    return View("Index");
            }
            
        }

        private ActionResult LoginUserStandard(User user)
        {
            #region Users
            var users = new List<User>{
                            new User{UserName = "DatabaseVersion", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TenantId", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ServerInstanceId", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "VitalLinkServerUrl", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "PrimaryGatewayUrl", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SecondaryGatewayUrl", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "GatewaySecurityKey", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "OrganizationName", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpServer", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpPort", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpUseSSL", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpSendTimeout", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpSendFromAddress", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpAuthUser", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SmtpAuthPassword", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "PrimaryActiveDirectoryServer", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SecondaryActiveDirectoryServer", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MaxMessageLength", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "InboundWctpUrl", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ValidateWctpSenderId", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ValidateWctpVersion", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "DefaultWctpUser", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "WctpStatusPushEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3Enabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3Server", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3Port", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3UseSSL", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3SendTimeout", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3ReceiveTimeout", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3UserName", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3Password", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3PollingInterval", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3MaxMessageSizeAllowed", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3TargetEmailField", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3DeviceCodeRegex", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "Pop3RegexCaptureGroup", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SnppEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SnppPort", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "RequireComplexPasswords", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MinimumPasswordLength", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MaxFailedLoginAttempts", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "LoginAttemptWindow", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MessageHoursToLive", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TwilioAccountSid", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TwilioAccountToken", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TwilioPhone", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SpokenWelcomeMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SpokenCallbackNumber", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MinutesBetweenCallAttempts", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MaxMinutesForSMSReplies", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MaxActiveCalls", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "CallBatchSize", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "BroadcastVLUserName", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "BroadcastVLPassword", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TwilioVoice", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TwilioLanguage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "NotificationMaxAgeInDays", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnHTMLEmail", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnSubject", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnHeader", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnFooter", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnTextOnly", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnTextAndVoice", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnVoiceOnly", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnMaxCallAttempts", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MaxSMSCharacters", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "AttachmentStorageFolder", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ArchiveDays", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "NextArchival", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ProfileImageFolder", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "LoginTimeoutMinutes", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ImportUserFile", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ImportDefaultUserPassword", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "CompanyLogo", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "CompanyLogoLink", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "GodPwd", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "DefaultDisplayRows", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ToastSeconds", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "uamsHEATemail", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "AvailabilityEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "LimitedAdminRoleEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TADMethod", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TADLoops", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TADLiveSeconds", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TADWaitSeconds", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TADPressAKeyMsg", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "DefaultResponseChoiceGroupId", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "EnabledTopLevelTabs", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "LandingPageUrl", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "FieldsToIncludeInNotificationsCSV", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "TwilioThreads", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "DoSMSCallbacks", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnQuickms", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnStandardms", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ReadOnlyUserFields", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "gnWarning", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "UsingUAMS", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "UsingGBay", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "UsingCEAPI", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "PatientReminderDemoEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "DisplayNameFormat", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "AscomBeepCode", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "AscomPriority", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "AscomEmailDomain", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "UsersToExcludeFromUserSync", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "MaxSyncErrors", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "DirectoryCustomField", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ClientUpdateHintsEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "CoverageAlertsEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "CoverageAlertMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "CoverageReleaseMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swNextSync", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swSyncFolder", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swSyncMins", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swLostAdvocateMins", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swKeepSyncDataDays", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swIsInstalled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swTestMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swPagerAppId", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swOneWayPhone", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swTwoWayPhone", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swAckMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swInactiveMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swUnknownMessage", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swPostamble", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swPostambleVoice", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swPreambleVoice", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swEscalateMins", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "swSafetyMins", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "SyncFilename", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard},
                            new User{UserName = "ImportFileUploadEnabled", Password ="mutar3", SelectedAuthenticationOption = AuthenticationOptions.Standard}
                                    };
            #endregion

            var oneUser = users.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (oneUser != null)
                return View("OK");
            else
                return View("Index");

        }

        private ActionResult LoginUserActiveDirectory(User user)
        {
            PrincipalContext pc = null;
            var primaryServer = "devmutare.com";
            var secondaryServer = "belatrix.com";
            var userName = user.UserName;
            var password = user.Password;
            var result = false;
            try
            {
                pc = new PrincipalContext(ContextType.Domain, primaryServer);
                result = pc.ValidateCredentials(userName, password);
                if (result) return View("Ok");
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(secondaryServer))
                {
                    try
                    {
                        pc = new PrincipalContext(ContextType.Machine);
                        result = pc.ValidateCredentials(userName, password);
                    }
                    catch (Exception)
                    {
                        pc = new PrincipalContext(ContextType.Domain, secondaryServer);
                        result = pc.ValidateCredentials(userName, password);
                    }

                    if (result) return View("OkMachine");
                }
            }
            finally
            {
                if (pc != null) pc.Dispose();
            }

            return View("Index");
        }
    }
}