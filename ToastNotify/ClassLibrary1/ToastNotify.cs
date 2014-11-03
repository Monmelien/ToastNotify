using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ToastNotify.ShellHelpers;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace ToastNotify
{
    public class Toast
    {
        private String shortcutPath;

        private String APP_ID = System.AppDomain.CurrentDomain.FriendlyName.Substring(0,System.AppDomain.CurrentDomain.FriendlyName.LastIndexOf(".")); 
        public ToastType type = ToastType.Text01; //Type de la toast forcer a text01 par défaut
        public System.Collections.Generic.List<string> text = new System.Collections.Generic.List<string>(); //Collection de string pour les 3 lignes
        
        public String image = ""; //Image de gauche sur la notification

        public String audio = ""; //Définition du son
        public bool silent;
        public bool loop;

        //Gestion des evenements
        public delegate void toastEventHandler(object sender, ToastEventArgs e);
        public event toastEventHandler toastActivated;
        public delegate void toastDissmissEventHandler(object sender, ToastDismissEventArgs e);
        public event toastDissmissEventHandler toastDismissed;
        public delegate void toastFailEventHandler(object sender, ToastFailEventArgs e);
        public event toastFailEventHandler toastFailed;

        [Flags]
        public enum ToastType
        { 
            ImageAndText01 = ToastTemplateType.ToastImageAndText01 ,
            ImageAndText02 = ToastTemplateType.ToastImageAndText02 ,
            ImageAndText03 = ToastTemplateType.ToastImageAndText03 ,
            ImageAndText04 = ToastTemplateType.ToastImageAndText04,
            Text01 = ToastTemplateType.ToastText01 ,
            Text02 = ToastTemplateType.ToastText02 ,
            Text03 = ToastTemplateType.ToastText03 ,
            Text04 = ToastTemplateType.ToastText04,
        };
        public ToastTemplateType TypeConvert(ToastType type)
        {
            switch (type)
            {
                case ToastType.ImageAndText01 :
                    return ToastTemplateType.ToastImageAndText01;
                case ToastType.ImageAndText02 :
                    return ToastTemplateType.ToastImageAndText02;
                case ToastType.ImageAndText03 :
                    return ToastTemplateType.ToastImageAndText03;
                case ToastType.ImageAndText04 :
                    return ToastTemplateType.ToastImageAndText04;
                case ToastType.Text01 :
                    return ToastTemplateType.ToastText01;
                case ToastType.Text02 :
                    return ToastTemplateType.ToastText02;
                case ToastType.Text03 :
                    return ToastTemplateType.ToastText03;
                case ToastType.Text04 :
                    return ToastTemplateType.ToastText04;

                default:
                    return ToastTemplateType.ToastImageAndText01;
            }
        }
        public ToastType TypeConvert(ToastTemplateType type)
        {
            switch (type)
            {
                case ToastTemplateType.ToastImageAndText01:
                    return ToastType.ImageAndText01;
                case ToastTemplateType.ToastImageAndText02:
                    return ToastType.ImageAndText02;
                case ToastTemplateType.ToastImageAndText03:
                    return ToastType.ImageAndText03;
                case ToastTemplateType.ToastImageAndText04:
                    return ToastType.ImageAndText04;
                case ToastTemplateType.ToastText01:
                    return ToastType.Text01;
                case ToastTemplateType.ToastText02:
                    return ToastType.Text02;
                case ToastTemplateType.ToastText03:
                    return ToastType.Text03;
                case ToastTemplateType.ToastText04:
                    return ToastType.Text04;

                default:
                    return ToastType.ImageAndText01;
            }
        }

        public Toast()
        {
            TryCreateShortcut();
        } //Constructeur
        ~Toast()
        {
            DelShortcut();
        } //Destructeur

        //Procédure local
        private void ToastActivated(ToastNotification sender, object e)
        {
            if (toastActivated != null)
                toastActivated(this, new ToastEventArgs("Hello"));
        }
        private void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
        {
            if (toastDismissed != null)
                toastDismissed(this, new ToastDismissEventArgs(e.Reason ));
        }
        private void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
        {
            
            if (toastFailed != null)
                toastFailed(this, new ToastFailEventArgs(e.ErrorCode));
            //        System.Windows.MessageBox.Show ("The toast encountered an error.");
        }


        private bool TryCreateShortcut()
        {
                String NameAppli = AppDomain.CurrentDomain.FriendlyName.ToString();
                NameAppli = NameAppli.Substring(0, NameAppli.LastIndexOf("."));

                shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\"+APP_ID+".lnk";

            if (!File.Exists(shortcutPath))
            {
                InstallShortcut();
                return true;
            }
            return false;
        }
        private void InstallShortcut()
        {
            // Find the path to the current executable
            String exePath = Process.GetCurrentProcess().MainModule.FileName;
            IShellLinkW newShortcut = (IShellLinkW)new CShellLink();
             
            // Create a shortcut to the exe
            ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
            ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

            // Open the shortcut property store, set the AppUserModelId property
            IPropertyStore newShortcutProperties = (IPropertyStore)newShortcut;

            using (PropVariant appId = new PropVariant(APP_ID))
            {
                ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));
                ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
            }

            // Commit the shortcut to disk
            String AppFolder = shortcutPath.Substring(0,shortcutPath.LastIndexOf("\\"));
            System.IO.Directory.CreateDirectory(AppFolder);
            IPersistFile newShortcutSave = (IPersistFile)newShortcut;

            ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutSave.Save(shortcutPath, true));
        }
        private void DelShortcut()
        {
            //suppression du raccourcis créer pour l'occasion.
            if (System.IO.File.Exists(shortcutPath))
            {
                System.IO.File.Delete(shortcutPath);
            }
        }
        // Create and show the toast.
        // See the "Toasts" sample for more detail on what can be done with toasts
        public void show()
        {
            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(TypeConvert(type));

            // Texte
            XmlNodeList stringElements;
            stringElements = toastXml.GetElementsByTagName("text");
            for (int i = 0; i < stringElements.Length; i++)
            {
                if (i<= text.Count-1) 
                    stringElements[i].AppendChild(toastXml.CreateTextNode(text[i]));
            }

            // Image
            if (image != "" && TypeConvert(type) != ToastTemplateType.ToastText01 && TypeConvert(type) != ToastTemplateType.ToastText02 && TypeConvert(type) != ToastTemplateType.ToastText03 && TypeConvert(type) != ToastTemplateType.ToastText04)
            {
                //String imagePath = "file:///" + image;
                String imagePath = "ms-appx:///" + image;
                XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            }
           
            // Audio
            if (audio != "")
            {
                XmlElement test = toastXml.CreateElement("audio");
                test.SetAttribute("loop", loop.ToString().ToLower() );
                test.SetAttribute("silent", silent.ToString().ToLower());
                test.SetAttribute("src", audio);

                toastXml.FirstChild.AppendChild(test);
            }

            // Toast & evenement
            ToastNotification toast = new ToastNotification(toastXml);
            toast.Activated += ToastActivated;
            toast.Dismissed += ToastDismissed;
            toast.Failed += ToastFailed;

            // Affichage de la toast
            MessageBox.Show(toastXml.GetXml());
            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
        }
    }

    [Flags]
    public enum toastDismissReason { ApplicationHidden = ToastDismissalReason.ApplicationHidden, UserCanceled = ToastDismissalReason.UserCanceled, TimedOut = ToastDismissalReason.TimedOut }

    public class ToastEventArgs
    {
        public String Text { get; private set; } // readonly

        public ToastEventArgs(string s)
        {
            Text = s;
        }
    }
    public class ToastDismissEventArgs
    {
        public toastDismissReason Reason { get; private set; }

        public ToastDismissEventArgs(ToastDismissalReason DissmissReason)
        {
            Reason = ReasonConvert(DissmissReason);
        }
        private toastDismissReason ReasonConvert(ToastDismissalReason Reason)
        {
            switch (Reason)
            {
                case ToastDismissalReason.ApplicationHidden :
                   return toastDismissReason.ApplicationHidden;
                case ToastDismissalReason.TimedOut :
                   return toastDismissReason.TimedOut;
                default : 
                   return toastDismissReason.UserCanceled;
            }
        }
    }
    public class ToastFailEventArgs
    {
        public Exception ErrorCode { get; private set; } // readonly

        public ToastFailEventArgs(Exception code)
        {
            ErrorCode = code;
        }
    }
}
