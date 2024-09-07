CREATE TABLE [dbo].[EventRule] (
    [EventRuleId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [ProductTypeId]        INT             NULL,
    [ModeOfDelivery]       INT             NULL,
    [SupplierLOBID]        BIGINT          NULL,
    [EventConfigurationID] BIGINT          NULL,
    [RuleFormula]          NVARCHAR (1000) NULL,
    [RuleDescription]      NVARCHAR (1000) NULL,
    [RuleType]             BIGINT          NULL,
    [Version]              NVARCHAR (50)   NULL,
    [IsActive]             BIT             NULL,
    [CreatedDate]          DATETIME        CONSTRAINT [DF_EventRule_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [CreatedBy]            NVARCHAR (50)   NOT NULL,
    [UpdatedDate]          DATETIME        NULL,
    [UpdatedBy]            NVARCHAR (50)   NULL,
    CONSTRAINT [PK_EventRule] PRIMARY KEY CLUSTERED ([EventRuleId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event rule was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event rule was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event rule is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Version of the rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'RuleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Description of the rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'RuleDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formula of the rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'RuleFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the associated event configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'EventConfigurationID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the supplier LOB (Line of Business).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'SupplierLOBID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mode of delivery.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'ModeOfDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the product type.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event rule.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRule', @level2type = N'COLUMN', @level2name = N'EventRuleId';

