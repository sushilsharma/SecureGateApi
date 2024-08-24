CREATE TABLE [dbo].[ChargeCalculationMethod] (
    [MethodId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [MethodName]  NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (255) NULL,
    CONSTRAINT [PK__ChargeCa__FC6818514E16CE0B] PRIMARY KEY CLUSTERED ([MethodId] ASC)
);

