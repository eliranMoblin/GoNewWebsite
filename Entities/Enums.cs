using System;

namespace Entities
{

    public enum UserEventType
    {
        ContactUsMessage = 10,
        EmailConfirmation = 8,
        EmailConfirmationReminder = 11,
        ForgotPassword = 2,
        HappyBirthday = 12,
        Sign_Out = 4,
        SignupToNewsletter = 7,
        SignupToNewsletterWithPop_UpFromExternal = 14,
        SignupToSystem = 1,
        SignuptosystemFromExternal = 9,
        Sing_In = 3,
        UpdateDetails = 6,
        UpdatePassword = 5,
    }

    public enum EventType
    {
        EmailToVendor = 1,
        EmailToClient = 2,
        Sms = 3
    }

    public enum EmailStatusType
    {
        Sent = 1,
        Bounse = 2,
        SpamReport = 3,
        Open = 4,
        Click = 5
    }


    public enum ApplicationType
    {
        Redirector = 1,
        PhoneApp = 2,
        Banner = 3,
        EmailToUser = 4,
        SMS = 5,
        OptSMS = 6,
        EmailConfirmation = 7
    }


    public enum CapabilityType
    {
        SMS = 1,
        Email = 2,
        Lead = 3,
        EmailToUser = 4,
        SmsOtp = 5,
        PushNotifications = 6
    }

    public enum ProviderType
    {
        Sms = 1,
        Smtp = 2

    }

    public enum DocumentType
    {
        WebsiteSetting = 1,
        Providers = 2,
        UserGroup = 4,
        AlertSettings = 5,
        Customer = 20,
        Application = 21,
        Campaign = 22,
        Contact = 23,
        MailSetting = 24,
        Condition = 25,
        Banner = 26,
        Template = 27,
        EmailToUser = 28,
        SmsToUser = 29,
        FtpSite = 30,
        CarouselBanner = 31,


        Lead = 70,
        VersionControl = 90
            , WebsitePage = 100
    }

    public enum LogType
    {
        HttpRequest = 1,
        Redirector = 2,
        Click = 3,
        SMSOtp = 4,
        PhoneApp = 5,
        VersionControl = 6
    }

    public enum CommonStatus
    {
        Disabled = 0,
        New = 1,
        Enabled = 2,
        Closed = 3,
        Suspended = 4,
        Testing = 5
    }

    public enum Theme
    {
        NoTheme = 1,
        //Christmas = 2,
        //Original = 3,
    }

    //public enum ProductType
    //{
    //    Ca
    //}

    public enum NotificationType
    {
        Success = 1,
        Info = 2,
        Warning = 3,
        Danger = 4

    }

    public enum UserStatus
    {
        New = 1,
        Active = 2,
        Suspended = 3,
        Blocked = 4,
        Closed = 5,
        Approved = 6
    }


    [Flags]
    public enum ImageOptions
    {
        GoogleCompression = 4,
        Crop = 16,
        Mobile = 32,
        Tablet = 64,
    }


    public enum ImageType
    {
        Logo = 1,
        Cover = 2,
        Screenshot = 3,
        BigCard = 4,
        Circle = 5,
        Card = 6,
        Other = 7,
        OpenGraph = 8,
        Background = 9,
        Facebook = 10,
        Twitter = 11,
        Article = 12,
    }


    public enum UserRoles
    {
        Admin = 0,
        Manager = 1,
        User = 2
    }

    public enum OperatingSystem
    {
        Windows = 1,
        Os = 2,
        Android = 3,
        Ios = 4
    }

    public enum Language
    {
        Hebrew = 1,
        English = 2,
        Arabic = 3
    }

    public enum CampaignType
    {
        LandingPage = 1,
        Banner = 2
    }

    public enum AppAction
    {
        Nothing_To_Do = 0,
        Update_Mandatory = 1,
        Update_Optional = 2,

    }

    public enum PooshWooshTagType
    {
        Integer = 1,
        String = 2,
        List = 3,
        Date = 4,
        Boolean = 5,
        Decimal = 6,
        Version = 7
    }

    public enum DeviceType
    {
        IOS = 1,
        BlackBerry = 2,
        Android = 3,
        WindowsPhone = 5,
        OSX = 7,
        Windows8 = 8,
        Amazon = 9,
        Safari = 10,
        Chrome = 11,
        Firefox = 12
    }
}

