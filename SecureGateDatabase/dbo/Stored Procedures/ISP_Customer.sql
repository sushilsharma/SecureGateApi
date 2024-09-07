CREATE PROCEDURE [dbo].[ISP_Customer] --'<Customers>   <Customer>     <CustomerName>Jane Doe</CustomerName>     <CustomerMnemonic>CUST002</CustomerMnemonic>     <CustomerType>Wholesale</CustomerType>     <ParentCustomer>NULL</ParentCustomer>     <AddressLine1>123 Elm Street</AddressLine1>     <AddressLine2>Suite 456</AddressLine2>     <AddressLine3>NULL</AddressLine3>     <City>Springfield</City>     <State>IL</State>     <CountryId>1</CountryId>     <Country>USA</Country>     <Postcode>62701</Postcode>     <Region>Midwest</Region>     <ZoneCode>MW01</ZoneCode>     <CategoryCode>CATEGORY01</CategoryCode>     <Email>janedoe@example.com</Email>     <SiteURL>http://example.com</SiteURL>     <ContactPersonNumber>555-1234</ContactPersonNumber>     <ContactPersonName>Jane Doe</ContactPersonName>     <logo>NULL</logo>     <header>NULL</header>     <footer>NULL</footer>     <CreatedBy>2</CreatedBy>     <CreatedDate>2024-09-07T00:00:00</CreatedDate>     <IsActive>1</IsActive>   </Customer> </Customers>'
    @XMLData XML
AS
BEGIN
    -- Declare a handle for the XML document
    DECLARE @DocHandle INT;

    -- Create an XML document handle
    EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XMLData;

    -- Insert data from the XML document into the Customer table
    INSERT INTO [dbo].[Customer] 
    (
        [CustomerName],
        [CustomerMnemonic],
        [CustomerType],
        [ParentCustomer],
        [AddressLine1],
        [AddressLine2],
        [AddressLine3],
        [City],
        [State],
        [CountryId],
        [Country],
        [Postcode],
        [Region],
        [ZoneCode],
        [CategoryCode],
        [Email],
        [SiteURL],
        [ContactPersonNumber],
        [ContactPersonName],
        [logo],
        [header],
        [footer],
        [CreatedBy],
        [CreatedDate],
        [IsActive]
    )
    SELECT 
        CustomerName,
        CustomerMnemonic,
        CustomerType,
        ParentCustomer, 
        AddressLine1,
        AddressLine2,
        AddressLine3,
        City,
        State,
        CountryId, 
        
        Country,
        Postcode,
        Region,
        ZoneCode,
        CategoryCode,
        Email,
        SiteURL,
        ContactPersonNumber,
        ContactPersonName,
        logo,
        header,
        footer,
        CreatedBy,
        (select dbo.GetDateLocal()),
        IsActive
    FROM OPENXML(@DocHandle, '/Customers/Customer', 2)
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
    );

    -- Clean up the XML document handle
    EXEC sp_xml_removedocument @DocHandle;

  -- SELECT @Enquiry as EnquiryId,@EnquiryAutoNumber as EnquiryAutoNumber FOR XML RAW('Json'),ELEMENTS
END