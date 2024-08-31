CREATE TABLE [dbo].[PageControl] (
    [PageControlId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [PageId]               BIGINT         NOT NULL,
    [ResourceKey]          NVARCHAR (500) NOT NULL,
    [ControlType]          INT            NULL,
    [ControlName]          NVARCHAR (500) NULL,
    [DataSource]           NVARCHAR (500) NULL,
    [DataType]             NVARCHAR (500) NULL,
    [DisplayName]          NVARCHAR (500) NULL,
    [IsActive]             BIT            NULL,
    [IsMandatory]          BIT            NULL,
    [ValidationExpression] NVARCHAR (500) NULL,
    CONSTRAINT [PK_PageControlMapping] PRIMARY KEY CLUSTERED ([PageControlId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the control is mandatory.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'IsMandatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the control is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Display name of the control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of data.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'DataType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Source of data for the control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'DataSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'ControlName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'ControlType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Key of the resource associated with the control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'ResourceKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the page associated with the control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'PageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the page control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageControl', @level2type = N'COLUMN', @level2name = N'PageControlId';

