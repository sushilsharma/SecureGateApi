CREATE TABLE [dbo].[LoginHistory] (
    [LoginHistoryId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [LoginId]                 BIGINT         NULL,
    [LogoutType]              NVARCHAR (100) NULL,
    [LoggingInTime]           DATETIME       NULL,
    [LoggingOutTime]          DATETIME       NULL,
    [TanentCode]              NVARCHAR (250) NULL,
    [CreatedDate]             DATETIME       CONSTRAINT [DF_LoginHistory_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [CreatedBy]               BIGINT         NULL,
    [CreatedFromIPAddress]    NVARCHAR (20)  NULL,
    [UpdatedDate]             DATETIME       NULL,
    [UpdatedBy]               BIGINT         NULL,
    [UpdatedFromIPAddress]    NVARCHAR (20)  NULL,
    [Username]                NVARCHAR (100) NULL,
    [DeviceLoggingInTime]     DATETIME       NULL,
    [DeviceLoggingOutTime]    DATETIME       NULL,
    [AddFrom]                 NVARCHAR (50)  NULL,
    [MACAddress]              NVARCHAR (100) NULL,
    [LogOutLatitude]          NVARCHAR (50)  NULL,
    [LogOutLongitude]         NVARCHAR (50)  NULL,
    [LoginLatitude]           NVARCHAR (50)  NULL,
    [LoginLongitude]          NVARCHAR (50)  NULL,
    [LoggingInDeviceUniqueId] NVARCHAR (100) NULL,
    [LogoutDeviceUniqueId]    NVARCHAR (100) NULL,
    [Guid]                    NVARCHAR (100) NULL,
    [Platform]                NVARCHAR (100) NULL,
    [Manufacturer]            NVARCHAR (200) NULL,
    [Model]                   NVARCHAR (200) NULL,
    [DeviceVersion]           NVARCHAR (200) NULL,
    [Serial]                  NVARCHAR (200) NULL,
    [AppVersionNo]            NVARCHAR (100) NULL,
    CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED ([LoginHistoryId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Version number of the application (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'AppVersionNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serial number of the device (nvarchar(200)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'Serial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Version of the device (nvarchar(200)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'DeviceVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Model of the device (nvarchar(200)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manufacturer of the device (nvarchar(200)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'Manufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Platform of the device (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'Platform';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID associated with the entry (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'Guid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier of the device used for logging out (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LogoutDeviceUniqueId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier of the device used for logging in (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoggingInDeviceUniqueId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitude of the login location (nvarchar(50)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoginLongitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Latitude of the login location (nvarchar(50)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoginLatitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitude of the logout location (nvarchar(50)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LogOutLongitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Latitude of the logout location (nvarchar(50)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LogOutLatitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MAC address of the device (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'MACAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Source from which the login was added (nvarchar(50)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'AddFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of logging out from the device (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'DeviceLoggingOutTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of logging in from the device (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'DeviceLoggingInTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Username associated with the login (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'Username';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP address from which the entry was last updated (nvarchar(20)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'UpdatedFromIPAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who last updated the entry (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the entry was last updated (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP address from which the entry was created (nvarchar(20)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'CreatedFromIPAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who created the entry (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the entry was created (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the entry is active (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of logging out (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoggingOutTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of logging in (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoggingInTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of logout (nvarchar(100)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LogoutType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the login associated with the history (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for each login history entry (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginHistory', @level2type = N'COLUMN', @level2name = N'LoginHistoryId';

