CREATE TABLE [dbo].[EventUserTypeMapping] (
    [EventUserType]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventCode]              NVARCHAR (250) NOT NULL,
    [UserType]               NCHAR (200)    NOT NULL,
    [DescriptionResourceKey] NVARCHAR (500) NOT NULL,
    [DisplayIcon]            NVARCHAR (MAX) NULL,
    [SequenceNumber]         INT            NULL,
    [CreatedBy]              BIGINT         NOT NULL,
    [CreatedDate]            DATETIME       CONSTRAINT [DF_EventUserTypeMapping_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [ModifiedBy]             BIGINT         NULL,
    [ModifiedDate]           DATETIME       CONSTRAINT [DF_EventUserTypeMapping_ModifiedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [IsActive]               BIT            CONSTRAINT [DF_EventUserTypeMapping_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_EventUserTypeMapping] PRIMARY KEY CLUSTERED ([EventUserType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event user type mapping is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the event user type mapping was last modified.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last modified the event user type mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the event user type mapping was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event user type mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sequence number to determine the order of display for user types.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'SequenceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Icon associated with the user type (can be stored as a binary data or a reference to an external resource).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'DisplayIcon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resource key for the description of the user type.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'DescriptionResourceKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the user associated with the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'UserType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event associated with the user type.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event user type mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventUserTypeMapping', @level2type = N'COLUMN', @level2name = N'EventUserType';

