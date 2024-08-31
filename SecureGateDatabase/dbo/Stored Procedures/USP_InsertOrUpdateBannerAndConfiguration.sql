CREATE PROCEDURE [dbo].[USP_InsertOrUpdateBannerAndConfiguration] --'<Root>   <AdvertisingBanner>     <AdvertisingBannerId>0</AdvertisingBannerId> <MemberId>123</MemberId>     <BannerName>My Banner</BannerName>     <BannerImage>path/to/image.jpg</BannerImage>     <FromDate>2024-09-01</FromDate>     <ToDate>2024-09-30</ToDate>     <Field1>Value1</Field1>     <IsActive>1</IsActive>     <Description>Banner description</Description>     <Title>Banner title</Title>     <Url>https://example.com</Url>     <TanentCode>ABC123</TanentCode> <UpdatedBy>1</UpdatedBy> <CreatedBy>1</CreatedBy>  <ConfigurableEntity>     <EntityId>0</EntityId> <EntityObjectType>Advertisement</EntityObjectType>     <EntityObjectId>0</EntityObjectId>  <EntityObjectType>Banner</EntityObjectType> <Description>Configuration details</Description>     <State>1</State>     <City>2</City>     <Pincode>401203</Pincode>     <RuleId>10</RuleId>     <IsActive>1</IsActive> <TanentCode>ABC123</TanentCode> <UpdatedBy>1</UpdatedBy> <CreatedBy>1</CreatedBy> </ConfigurableEntity> <ConfigurableEntity>     <EntityId>0</EntityId> <EntityObjectType>Advertisement</EntityObjectType>     <EntityObjectId>0</EntityObjectId>  <EntityObjectType>Banner</EntityObjectType> <Description>Configuration details</Description>     <State>1</State>     <City>2</City>     <Pincode>401203</Pincode>     <RuleId>10</RuleId>     <IsActive>1</IsActive> <TanentCode>ABC123</TanentCode> <UpdatedBy>1</UpdatedBy> <CreatedBy>1</CreatedBy>  </ConfigurableEntity>  </AdvertisingBanner>    </Root>'
(
    @XMLData XML
)
AS
BEGIN
    DECLARE @AdvertisingBannerId BIGINT;
    DECLARE @IsUpdate BIT;

    BEGIN TRANSACTION;

    -- Parse the XML data
    DECLARE @hDoc INT;
    EXEC sp_xml_preparedocument @hDoc OUTPUT, @XMLData;

    -- Check if AdvertisingBanner exists
    SELECT @AdvertisingBannerId = AdvertisingBannerId
    FROM OPENXML(@hDoc, '/Root/AdvertisingBanner', 2) WITH
    (
        AdvertisingBannerId INT
    ) Banner;

    -- Determine whether to update or insert
    SET @IsUpdate = CASE WHEN ISNULL(@AdvertisingBannerId,0)=0 THEN 0 ELSE 1 END;

    -- Update or insert into AdvertisingBanner table
    IF @IsUpdate = 1
    BEGIN
        UPDATE AdvertisingBanner
        SET
            MemberId = Banner.MemberId,
            BannerName = Banner.BannerName,
            BannerImage = Banner.BannerImage,
            FromDate = Banner.FromDate,
            ToDate = Banner.ToDate,
            Field1 = Banner.Field1,
            Field2 = Banner.Field2,
            Field3 = Banner.Field3,
            Field4 = Banner.Field4,
            Field5 = Banner.Field5,
            Field6 = Banner.Field6,
            Field7 = Banner.Field7,
            Field8 = Banner.Field8,
            Field9 = Banner.Field9,
            Field10 = Banner.Field10,
            IsActive = Banner.IsActive,
            Description = Banner.Description,
            Title = Banner.Title,
            Url = Banner.Url,
            TanentCode = Banner.TanentCode,
			UpdatedDate = dbo.GetDateLocal(),
            UpdatedBy = Banner.UpdatedBy
			
        FROM OPENXML(@hDoc, '/Root/AdvertisingBanner', 2) WITH
        (
            AdvertisingBannerId BIGINT,
            MemberId BIGINT,
            BannerName NVARCHAR(150),
            BannerImage NVARCHAR(500),
            FromDate DATETIME,
            ToDate DATETIME,
            Field1 NVARCHAR(50),
            Field2 NVARCHAR(50),
            Field3 NVARCHAR(50),
            Field4 NVARCHAR(50),
            Field5 NVARCHAR(50),
            Field6 NVARCHAR(50),
            Field7 NVARCHAR(50),
            Field8 NVARCHAR(50),
            Field9 NVARCHAR(50),
            Field10 NVARCHAR(50),
            IsActive BIT,
            Description NVARCHAR(MAX),
            Title NVARCHAR(500),
            Url NVARCHAR(500),
            TanentCode NVARCHAR(250),
			UpdatedBy BIGINT
        ) Banner
        WHERE AdvertisingBanner.AdvertisingBannerId = @AdvertisingBannerId;
    END
    ELSE
    BEGIN
        INSERT INTO AdvertisingBanner (
            MemberId,
            BannerName,
            BannerImage,
            FromDate,
            ToDate,
            Field1,
            Field2,
            Field3,
            Field4,
            Field5,
            Field6,
            Field7,
            Field8,
            Field9,
            Field10,
            IsActive,
            Description,
            Title,
            Url,
            TanentCode,
			CreatedDate,
            CreatedBy 
        )
        SELECT
            Banner.MemberId,
            Banner.BannerName,
            Banner.BannerImage,
            Banner.FromDate,
            Banner.ToDate,
            Banner.Field1,
            Banner.Field2,
            Banner.Field3,
            Banner.Field4,
            Banner.Field5,
            Banner.Field6,
            Banner.Field7,
            Banner.Field8,
            Banner.Field9,
            Banner.Field10,
            Banner.IsActive,
            Banner.Description,
            Banner.Title,
            Banner.Url,
            Banner.TanentCode,
			dbo.GetDateLocal(),
            Banner.CreatedBy 
        FROM OPENXML(@hDoc, '/Root/AdvertisingBanner', 2) WITH
        (
            MemberId BIGINT,
            BannerName NVARCHAR(150),
            BannerImage NVARCHAR(500),
            FromDate DATETIME,
            ToDate DATETIME,
            Field1 NVARCHAR(50),
            Field2 NVARCHAR(50),
            Field3 NVARCHAR(50),
            Field4 NVARCHAR(50),
            Field5 NVARCHAR(50),
            Field6 NVARCHAR(50),
            Field7 NVARCHAR(50),
            Field8 NVARCHAR(50),
            Field9 NVARCHAR(50),
            Field10 NVARCHAR(50),
            IsActive BIT,
            Description NVARCHAR(MAX),
            Title NVARCHAR(500),
            Url NVARCHAR(500),
            TanentCode NVARCHAR(250),
			CreatedDate DATETIME,
            CreatedBy BIGINT
        ) Banner;

        SET @AdvertisingBannerId = SCOPE_IDENTITY();
    END

    -- Update or insert into ConfigurableEntity table (multiple times)
    IF @IsUpdate = 1
    BEGIN
        UPDATE ConfigurableEntity
        SET
            Description = Config.Description,
            State = Config.State,
            City = Config.City,
            Pincode = Config.Pincode,
            RuleId = Config.RuleId,
            IsActive = Config.IsActive,
            UpdatedDate = dbo.GetDateLocal(),
            UpdatedBy = Config.UpdatedBy
        FROM OPENXML(@hDoc, '/Root/AdvertisingBanner/ConfigurableEntity', 2) WITH
        (
		
            EntityId BIGINT,
			EntityObjectType NVARCHAR(250),
            EntityObjectId bigint,
            Description NVARCHAR(MAX),
            State BIGINT,
            City BIGINT,
            Pincode NVARCHAR(100),
            RuleId BIGINT,
			UpdatedBy BIGINT,
            IsActive BIT
        ) Config
        WHERE ConfigurableEntity.EntityId = Config.EntityId AND ConfigurableEntity.EntityObjectId = @AdvertisingBannerId 
		and ConfigurableEntity.EntityObjectType = Config.EntityObjectType;
    END
    ELSE
    BEGIN
        INSERT INTO ConfigurableEntity (
            EntityObjectType,
            EntityObjectId,
            Description,
            State,
            City,
            Pincode,
            RuleId,
            IsActive,
            CreatedDate,
            CreatedBy,
            TanentCode
        )
        SELECT
            EntityObjectType,
            @AdvertisingBannerId,
            Description,
            State,
            City,
            Pincode,
            RuleId,
            IsActive,
            dbo.GetDateLocal(),
            CreatedBy,
            TanentCode
            
        FROM OPENXML(@hDoc, '/Root/AdvertisingBanner/ConfigurableEntity', 2) WITH
        (
            EntityId BIGINT,
            EntityObjectType NVARCHAR(250),
            EntityObjectId BIGINT,
            Description NVARCHAR(MAX),
            State BIGINT,
            City BIGINT,
            Pincode NVARCHAR(100),
            RuleId BIGINT,
            IsActive BIT,
            CreatedDate DATETIME,
            CreatedBy BIGINT,
            TanentCode NVARCHAR(250)
        ) Config;
    END

    EXEC sp_xml_removedocument @hDoc;

    COMMIT TRANSACTION;

    SELECT @AdvertisingBannerId AS 'AdvertisingBannerId';
END;