
-- Create the stored procedure
CREATE PROCEDURE [dbo].[USP_Customer]
    @XMLData XML
AS
BEGIN
    -- Declare a handle for the XML document
    DECLARE @DocHandle INT;
	DECLARE @CustomerMnemonic NVARCHAR(250);
	DECLARE @CustomerId bigint;
    -- Create an XML document handle
    EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XMLData;


	 SELECT 
        @CustomerId = tmp.CustomerId,
		@CustomerMnemonic=tmp.CustomerMnemonic
    FROM OPENXML(@DocHandle, '/Customers/Customer', 2)
    WITH (
        CustomerMnemonic NVARCHAR(250),
        CustomerId BIGINT
    ) tmp;


    -- Update data from the XML document in the Customer table
    UPDATE [Customer]
    SET 
        [CustomerName] = x.CustomerName,
        [CustomerMnemonic] = x.CustomerMnemonic,
        [CustomerType] = x.CustomerType,
        [ParentCustomer] = x.ParentCustomer,
        [AddressLine1] = x.AddressLine1,
        [AddressLine2] = x.AddressLine2,
        [AddressLine3] = x.AddressLine3,
        [City] = x.City,
        [State] = x.State,
        [CountryId] = x.CountryId,
        [Country] = x.Country,
        [Postcode] = x.Postcode,
        [Region] = x.Region,
        [ZoneCode] = x.ZoneCode,
        [CategoryCode] = x.CategoryCode,
        [Email] = x.Email,
        [SiteURL] = x.SiteURL,
        [ContactPersonNumber] = x.ContactPersonNumber,
        [ContactPersonName] = x.ContactPersonName,
        [logo] = x.logo,
        [header] = x.header,
        [footer] = x.footer,
        [CreatedBy] = x.CreatedBy,
        [CreatedDate] = x.CreatedDate,
        [IsActive] = x.IsActive
    FROM [dbo].[Customer] c
    JOIN OPENXML(@DocHandle, '/Customers/Customer', 2)
    WITH (
        CustomerName NVARCHAR(500),
        CustomerMnemonic NVARCHAR(200),
        CustomerType NVARCHAR(50),
        ParentCustomer BIGINT,
        AddressLine1 NVARCHAR(100),
        AddressLine2 NVARCHAR(100),
        AddressLine3 NVARCHAR(100),
        City NVARCHAR(100),
        State NVARCHAR(100),
        CountryId BIGINT,
        Country NVARCHAR(50),
        Postcode NVARCHAR(20),
        Region NVARCHAR(50),
        ZoneCode NVARCHAR(20),
        CategoryCode NVARCHAR(20),
        Email NVARCHAR(MAX),
        SiteURL NVARCHAR(200),
        ContactPersonNumber NVARCHAR(20),
        ContactPersonName NVARCHAR(500),
        logo NVARCHAR(MAX),
        header NVARCHAR(MAX),
        footer NVARCHAR(MAX),
        CreatedBy BIGINT,
        CreatedDate DATETIME,
        IsActive BIT
    ) x
    ON c.CustomerId =@CustomerId;

    -- Clean up the XML document handle
    EXEC sp_xml_removedocument @DocHandle;
	SELECT @CustomerId as CustomerId,@CustomerMnemonic as CustomerMnemonic FOR XML RAW('Json'),ELEMENTS
END;