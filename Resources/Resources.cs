    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Resources.Abstract;
    using Resources.Concrete;
    
    namespace Resources {
        public class Resources {
            private static IResourceProvider resourceProvider = new DbResourceProvider(); //  new XmlResourceProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Resources.xml"));

            /// <summary>Add person</summary>
            public static string AddPerson
            {
                get
                {
                    return resourceProvider.GetResource("AddPerson", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Age</summary>
            public static string Age
            {
                get
                {
                    return resourceProvider.GetResource("Age", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Must be between 10 and 130</summary>
            public static string AgeRange
            {
                get
                {
                    return resourceProvider.GetResource("AgeRange", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Age is required</summary>
            public static string AgeRequired
            {
                get
                {
                    return resourceProvider.GetResource("AgeRequired", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Biography</summary>
            public static string Biography
            {
                get
                {
                    return resourceProvider.GetResource("Biography", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Choose your language</summary>
            public static string ChooseYourLanguage
            {
                get
                {
                    return resourceProvider.GetResource("ChooseYourLanguage", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Create</summary>
            public static string Create
            {
                get
                {
                    return resourceProvider.GetResource("Create", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Email</summary>
            public static string Email => (string) resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name);

            /// <summary>Email is not valid</summary>
            public static string EmailInvalid
            {
                get
                {
                    return resourceProvider.GetResource("EmailInvalid", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Email is required</summary>
            public static string EmailRequired
            {
                get
                {
                    return resourceProvider.GetResource("EmailRequired", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>First name</summary>
            public static string FirstName
            {
                get
                {
                    return resourceProvider.GetResource("FirstName", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Must be less than 50 characters</summary>
            public static string FirstNameLong
            {
                get
                {
                    return resourceProvider.GetResource("FirstNameLong", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>First name is required</summary>
            public static string FirstNameRequired
            {
                get
                {
                    return resourceProvider.GetResource("FirstNameRequired", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Last name</summary>
            public static string LastName
            {
                get
                {
                    return resourceProvider.GetResource("LastName", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Must be less than 50 characters</summary>
            public static string LastNameLong
            {
                get
                {
                    return resourceProvider.GetResource("LastNameLong", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Last name is required</summary>
            public static string LastNameRequired
            {
                get
                {
                    return resourceProvider.GetResource("LastNameRequired", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Log off</summary>
            public static string LogOff
            {
                get
                {
                    return resourceProvider.GetResource("LogOff", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Log on</summary>
            public static string LogOn
            {
                get
                {
                    return resourceProvider.GetResource("LogOn", CultureInfo.CurrentUICulture.Name) as String;
                }
            }

            /// <summary>Register</summary>
            public static string Register
            {
                get
                {
                    return resourceProvider.GetResource("Register", CultureInfo.CurrentUICulture.Name) as String;
                }
            }
     
        }        
}
