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
        //shortcut ? 
        private String shortcutPath;
        
        // variable de base
        private String APP_ID = System.AppDomain.CurrentDomain.FriendlyName.Substring(0,System.AppDomain.CurrentDomain.FriendlyName.LastIndexOf("."));
        
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

        //Sauvegarde de l'object
        public System.Collections.Generic.List<String> SaveText = new System.Collections.Generic.List<String>();  //Collection de string pour les 3 lignes
        private String SaveImage; //Image de gauche sur la notification
        private String SaveAudio = ""; //Définition du son

        //Variable Text du ToastXml
        private XmlDocument toastXml;
        public String text1
        {
            set
            {
                XmlNodeList stringElements;
                stringElements = toastXml.GetElementsByTagName("text");

                SaveText.Insert(0, value);
                for (int i = 0; i < stringElements.Length; i++)
                {
                    string NodeValue = stringElements[i].Attributes.Item(0).NodeValue.ToString();
                    if (NodeValue.Equals("1"))
                    {
                        stringElements[i].AppendChild(toastXml.CreateTextNode(value));
                    }
                }
            }
            get
            {
                XmlNodeList stringElements;
                stringElements = toastXml.GetElementsByTagName("text");

                
                for (int i = 0; i < stringElements.Length; i++)
                {
                    
                    string NodeValue = stringElements[i].Attributes.Item(0).NodeValue.ToString();
                    if (NodeValue.Equals("1"))
                    {
                        return stringElements[i].FirstChild.GetXml() ;
                    }
                }
                return "";
            }
        }
        public String text2
        {
            set
            {
                XmlNodeList stringElements;
                stringElements = toastXml.GetElementsByTagName("text");

                SaveText.Insert(1, value);
                for (int i = 0; i < stringElements.Length; i++)
                {
                    
                    string NodeValue = stringElements[i].Attributes.Item(0).NodeValue.ToString();
                    if (NodeValue.Equals("2"))
                    {
                        stringElements[i].AppendChild(toastXml.CreateTextNode(value));
                    }
                }
            }
            get
            {
                XmlNodeList stringElements;
                stringElements = toastXml.GetElementsByTagName("text");


                for (int i = 0; i < stringElements.Length; i++)
                {

                    string NodeValue = stringElements[i].Attributes.Item(0).NodeValue.ToString();
                    if (NodeValue.Equals("2"))
                    {
                        return stringElements[i].FirstChild.GetXml();

                    }
                }
                return "";
            }
        }
        public String text3
        {
            set
            {
                XmlNodeList stringElements;
                stringElements = toastXml.GetElementsByTagName("text");

                SaveText.Insert(2, value);
                for (int i = 0; i < stringElements.Length; i++)
                {

                    string NodeValue = stringElements[i].Attributes.Item(0).NodeValue.ToString();
                    if (NodeValue.Equals("3"))
                    {
                        stringElements[i].AppendChild(toastXml.CreateTextNode(value));
                    }
                }
            }
            get
            {
                XmlNodeList stringElements;
                stringElements = toastXml.GetElementsByTagName("text");


                for (int i = 0; i < stringElements.Length; i++)
                {

                    MessageBox.Show(toastXml.GetXml());
                    string NodeValue = stringElements[i].Attributes.Item(0).NodeValue.ToString();
                    if (NodeValue.Equals("3"))
                    {
                        return stringElements[i].FirstChild.GetXml();
                    }
                }
                return "";
            }
        }
        public System.Collections.Generic.List<String> text  //Collection de string pour les 3 lignes
        {
            set
            {
                for (int i = 0; i <= 2; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (i < value.Count)
                                text1 = value[i];
                            else
                                text1 = "";
                            break;
                        case 1:
                            if (i < value.Count)
                                text2 = value[i];
                            else
                                text2 = "";
                            break;
                        case 2:
                            if (i < value.Count)
                                text3 = value[i];
                            else
                                text3 = "";
                            break;

                        default:
                            break;
                    }

                }
            }
            get
            {
                return new System.Collections.Generic.List<string>() { text1, text2, text3 };
            }
        }

        //Variable Image
        public String Image
        {
            set
            {
                if (value != "" && HaveImage())
                {
                    SaveImage = value;
                    //String imagePath = "file:///" + value;
                    String imagePath = "ms-appx:///" + value;
                    //String imagePath = "ms-appdata:///local/" + value; 
                    XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                    imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
                }
            }
            get
            {
                return "Youpi Ho!!!!";
            }
        }
        
        public bool HaveImage()
        {
            if (type == ToastType.ImageAndText01 || type == ToastType.ImageAndText02 || type == ToastType.ImageAndText03 || type == ToastType.ImageAndText04)
            return true;
            else
            return false;
        }
        
        private ToastType prType; 
        public ToastType type
        {
            set
            {
                prType = value;
                toastXml = ToastNotificationManager.GetTemplateContent(TypeConvert(value));
            }
            get
            {
                return prType;
            }
        } //Type de toasts

        public String GetXml()
        {
            return toastXml.GetXml();
        }

        public Toast(ToastType TypeToast)
        {
            // Création du raccourcis pour l'utilisation des toast
            TryCreateShortcut();

            // Création de la toast via le Type
            type = TypeToast;
        } //Constructeur
        ~Toast()
        {
            DelShortcut();
        } //Destructeur

        //Procédure local
        private void ToastActivated(ToastNotification sender, object e)
        {
            if (toastActivated != null)
                toastActivated(this, new ToastEventArgs(""));
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

        public void show()
        {
            // Image
            //if (image != "" && TypeConvert(type) != ToastTemplateType.ToastText01 && TypeConvert(type) != ToastTemplateType.ToastText02 && TypeConvert(type) != ToastTemplateType.ToastText03 && TypeConvert(type) != ToastTemplateType.ToastText04)
            //{
            //    //String imagePath = "file:///" + image;
            //    String imagePath = "ms-appx:///" + image;
            //    XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            //    imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            //}
           
            //// Audio
            //if (audio != "")
            //{
            //    XmlElement test = toastXml.CreateElement("audio");
            //    test.SetAttribute("loop", loop.ToString().ToLower() );
            //    test.SetAttribute("silent", silent.ToString().ToLower());
            //    test.SetAttribute("src", audio);

            //    toastXml.FirstChild.AppendChild(test);
            //}

            // Toast & evenement
            ToastNotification toast = new ToastNotification(toastXml);
            toast.Activated += ToastActivated;
            toast.Dismissed += ToastDismissed;
            toast.Failed += ToastFailed;
           

            // Affichage de la toast
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
