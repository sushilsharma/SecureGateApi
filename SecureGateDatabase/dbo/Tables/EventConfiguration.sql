CREATE TABLE [dbo].[EventConfiguration] (
    [EventConfigurationID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [SupplierLOBId]        BIGINT        NULL,
    [Process]              NVARCHAR (50) NOT NULL,
    [EventCode]            NVARCHAR (50) NOT NULL,
    [IsRequired]           BIT           CONSTRAINT [DF_EventConfiguration_IsRequired] DEFAULT ((1)) NULL,
    [SequenceNumber]       INT           NULL,
    [StockLocationID]      BIGINT        NULL,
    [TransportOperatorID]  BIGINT        NULL,
    [PlaceOfExecution]     INT           NULL,
    CONSTRAINT [PK_EventConfiguration] PRIMARY KEY CLUSTERED ([EventConfigurationID] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Place of execution for the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'PlaceOfExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the transport operator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'TransportOperatorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the stock location.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'StockLocationID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sequence number of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'SequenceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event is required.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'IsRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Process associated with the event configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the supplier line of business.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'SupplierLOBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventConfiguration', @level2type = N'COLUMN', @level2name = N'EventConfigurationID';

