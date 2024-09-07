CREATE TABLE [dbo].[Customer] (
    [CustomerId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CustomerName]        NVARCHAR (500) NOT NULL,
    [CustomerMnemonic]    NVARCHAR (200) NULL,
    [CustomerType]        NVARCHAR (50)  NOT NULL,
    [ParentCustomer]      BIGINT         NULL,
    [AddressLine1]        NVARCHAR (100) NULL,
    [AddressLine2]        NVARCHAR (100) NULL,
    [AddressLine3]        NVARCHAR (100) NULL,
    [City]                NVARCHAR (100) NULL,
    [State]               NVARCHAR (100) NULL,
    [CountryId]           BIGINT         NOT NULL,
    [Country]             NVARCHAR (50)  NULL,
    [Postcode]            NVARCHAR (20)  NULL,
    [Region]              NVARCHAR (50)  NULL,
    [ZoneCode]            NVARCHAR (20)  NULL,
    [CategoryCode]        NVARCHAR (20)  NULL,
    [Email]               NVARCHAR (MAX) NULL,
    [SiteURL]             NVARCHAR (200) NULL,
    [ContactPersonNumber] NVARCHAR (20)  NULL,
    [ContactPersonName]   NVARCHAR (500) NULL,
    [logo]                NVARCHAR (MAX) NULL,
    [header]              NVARCHAR (MAX) NULL,
    [footer]              NVARCHAR (MAX) NULL,
    [CreatedBy]           BIGINT         NOT NULL,
    [CreatedDate]         DATETIME       CONSTRAINT [DF_Customer_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [ModifiedBy]          BIGINT         NULL,
    [ModifiedDate]        DATETIME       CONSTRAINT [DF_Customer_ModifiedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [IsActive]            BIT            NOT NULL,
    [Field2]              NVARCHAR (500) NULL,
    [Field3]              NVARCHAR (500) NULL,
    [Field4]              NVARCHAR (500) NULL,
    [Field5]              NVARCHAR (500) NULL,
    [Field6]              NVARCHAR (500) NULL,
    [Field7]              NVARCHAR (500) NULL,
    [Field8]              NVARCHAR (500) NULL,
    [Field9]              NVARCHAR (500) NULL,
    [Field10]             NVARCHAR (500) NULL,
    [PaymentTermCode]     BIGINT         NULL,
    [CategoryType]        BIGINT         NULL,
    [DistrictName]        NVARCHAR (200) NULL,
    [CountryCode]         NVARCHAR (200) NULL,
    [CityName]            NVARCHAR (200) NULL,
    [TanentCode]          BIGINT         NULL,
    [PaymentTerm]         BIGINT         NULL,
    [Priority]            BIGINT         NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC, [CustomerType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of category', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CategoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code representing the payment term of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'PaymentTermCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Field2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the Customer is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the Customer was last modified.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who last modified the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the Customer was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who created the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Footer of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'footer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Header of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'header';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Logo of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'logo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the contact person representing the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ContactPersonName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contact number of the person representing the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ContactPersonNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL of the Customer''s website.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'SiteURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email address of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code representing the category of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CategoryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code representing the zone of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ZoneCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Region of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Region';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Postal code of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Postcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Country of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Country';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the country to which the Customer belongs.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CountryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'State of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'City of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Third line of the address.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'AddressLine3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Second line of the address.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'AddressLine2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'First line of the address.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'AddressLine1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the parent Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ParentCustomer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CustomerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mnemonic code representing the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CustomerMnemonic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CustomerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the Customer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CustomerId';

