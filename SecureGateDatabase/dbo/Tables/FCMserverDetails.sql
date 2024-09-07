CREATE TABLE [dbo].[FCMserverDetails] (
    [FCMServerDetailsId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [AppCode]            NVARCHAR (100) NULL,
    [AppName]            NVARCHAR (500) NULL,
    [DeviceType]         NVARCHAR (50)  NULL,
    [AppPackageName]     NVARCHAR (500) NULL,
    [ServerKey]          NVARCHAR (MAX) NULL,
    [SenderID]           NVARCHAR (500) NULL,
    [IsActive]           BIT            NOT NULL,
    [CreatedBy]          BIGINT         NOT NULL,
    [CreatedDate]        DATETIME       CONSTRAINT [DF_FCMserverDetails_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]          BIGINT         NULL,
    [UpdatedDate]        DATETIME       NULL,
    CONSTRAINT [PK_FCMserverDetails] PRIMARY KEY CLUSTERED ([FCMServerDetailsId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the record was updated (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who updated the record (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the record was created (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the creator (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicator for the active status (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID of the sender (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'SenderID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Key of the server (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'ServerKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Package name of the application (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'AppPackageName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the device (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'DeviceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the application (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'AppName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the application (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'AppCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for FCM server details (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMserverDetails', @level2type = N'COLUMN', @level2name = N'FCMServerDetailsId';

