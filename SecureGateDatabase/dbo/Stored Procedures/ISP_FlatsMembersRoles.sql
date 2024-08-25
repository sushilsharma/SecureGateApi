CREATE PROCEDURE [dbo].[ISP_FlatsMembersRoles] --'<Root>     <Flats>         <Flat><FlatId>1</FlatId><FlatNo>101</FlatNo><Wing>A</Wing><TotalArea>120.50</TotalArea><RoomTypeId>2</RoomTypeId><TanentCode>ABC123</TanentCode><IsActive>1</IsActive><CreatedDate>2024-08-25T12:00:00</CreatedDate><CreatedBy>1</CreatedBy><UpdatedDate>2024-08-25T12:00:00</UpdatedDate><UpdatedBy>1</UpdatedBy>         </Flat>     </Flats>     <Members>         <Member><MemberId>1</MemberId><Name>John Doe</Name><ContactNumber>1234567890</ContactNumber><Email>johndoe@example.com</Email><CountryCode>+1</CountryCode><PasswordHash>hashedpassword</PasswordHash><PasswordSalt>salt</PasswordSalt><ConfirmationToken>token123</ConfirmationToken><TokenGenerationTime>2024-08-25T12:00:00</TokenGenerationTime><PasswordRecoveryToken>recoverytoken123</PasswordRecoveryToken><RecoveryTokenTime>2024-08-25T12:00:00</RecoveryTokenTime><FlatId>1</FlatId><TanentCode>ABC123</TanentCode><IsActive>1</IsActive><CreatedDate>2024-08-25T12:00:00</CreatedDate><CreatedBy>1</CreatedBy><UpdatedDate>2024-08-25T12:00:00</UpdatedDate><UpdatedBy>1</UpdatedBy><FailedLoginAttempts>0</FailedLoginAttempts><LastLoginTime>2024-08-25T12:00:00</LastLoginTime><AccountLockTime>2024-08-25T12:00:00</AccountLockTime><IsAccountLock>0</IsAccountLock>         </Member>     </Members>     <MemberRoles>         <MemberRole><MemberRoleId>1</MemberRoleId><MemberId>1</MemberId><RoleMasterId>1</RoleMasterId><TanentCode>ABC123</TanentCode><IsActive>1</IsActive><CreatedDate>2024-08-25T12:00:00</CreatedDate><CreatedBy>1</CreatedBy><UpdatedDate>2024-08-25T12:00:00</UpdatedDate><UpdatedBy>1</UpdatedBy>         </MemberRole>     </MemberRoles> </Root>'
(
    @xmlDoc xml
)
AS
BEGIN
    SET ARITHABORT ON;
    DECLARE @TranName NVARCHAR(255);
    DECLARE @ErrMsg NVARCHAR(2048);
    DECLARE @ErrSeverity INT;
    DECLARE @intPointer INT;
    SET @ErrSeverity = 15;

    BEGIN TRY
        -- Parse the XML document
        EXEC sp_xml_preparedocument @intPointer OUTPUT, @xmlDoc;

        -- Handling Flats
        DECLARE @FlatId BIGINT;
        DECLARE @FlatNo NVARCHAR(10);
        DECLARE @Wing NVARCHAR(10);
        DECLARE @TotalArea DECIMAL(10,2);
        DECLARE @RoomTypeId BIGINT;
        DECLARE @TanentCode NVARCHAR(250);
        DECLARE @IsActive BIT;
        DECLARE @CreatedDate DATETIME;
        DECLARE @CreatedBy BIGINT;
        DECLARE @UpdatedDate DATETIME;
        DECLARE @UpdatedBy BIGINT;
		    -- Handling MemberRoles
        DECLARE @MemberRoleId BIGINT;
   
        DECLARE @RoleMasterId BIGINT;

		 -- Handling Members
        DECLARE @MemberId BIGINT;
        DECLARE @Name NVARCHAR(100);
        DECLARE @ContactNumber NVARCHAR(100);
        DECLARE @Email NVARCHAR(100);
        DECLARE @CountryCode NVARCHAR(100);
        DECLARE @PasswordHash NVARCHAR(250);
        DECLARE @PasswordSalt NVARCHAR(100);
        DECLARE @ConfirmationToken NVARCHAR(100);
        DECLARE @TokenGenerationTime DATETIME;
        DECLARE @PasswordRecoveryToken NVARCHAR(100);
        DECLARE @RecoveryTokenTime DATETIME;
   
        DECLARE @FailedLoginAttempts INT;
        DECLARE @LastLoginTime DATETIME;
        DECLARE @AccountLockTime DATETIME;
        DECLARE @IsAccountLock BIT;

        -- Insert or Update Flats
        SELECT
            @FlatNo = tmp.[FlatNo],
            @Wing = tmp.[Wing],
            @TotalArea = tmp.[TotalArea],
            @RoomTypeId = tmp.[RoomTypeId],
            @TanentCode = tmp.[TanentCode],
            @IsActive = tmp.[IsActive],
            @CreatedDate = tmp.[CreatedDate],
            @CreatedBy = tmp.[CreatedBy],
            @UpdatedDate = tmp.[UpdatedDate],
            @UpdatedBy = tmp.[UpdatedBy]
        FROM OPENXML(@intPointer, 'Root/Flats/Flat', 2)
        WITH
        (
            [FlatId] BIGINT,
            [FlatNo] NVARCHAR(10),
            [Wing] NVARCHAR(10),
            [TotalArea] DECIMAL(10,2),
            [RoomTypeId] BIGINT,
            [TanentCode] NVARCHAR(250),
            [IsActive] BIT,
            [CreatedDate] DATETIME,
            [CreatedBy] BIGINT,
            [UpdatedDate] DATETIME,
            [UpdatedBy] BIGINT
        ) tmp;

        IF EXISTS (SELECT 1 FROM dbo.Flats WHERE FlatNo = @FlatNo and Wing= @Wing and TanentCode=@TanentCode)
        BEGIN
            UPDATE dbo.Flats
            SET
                FlatNo = @FlatNo,
                Wing = @Wing,
                TotalArea = @TotalArea,
                RoomTypeId = @RoomTypeId,
                TanentCode = @TanentCode,
                IsActive = @IsActive,
                UpdatedDate = (select dbo.GetDateLocal()),
                UpdatedBy = @UpdatedBy
            WHERE FlatId = @FlatId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.Flats
            (
                FlatNo,
                Wing,
                TotalArea,
                RoomTypeId,
                TanentCode,
                IsActive,
                CreatedDate,
                CreatedBy
            )
            VALUES
            (
                @FlatNo,
                @Wing,
                @TotalArea,
                @RoomTypeId,
                @TanentCode,
                @IsActive,
                (select dbo.GetDateLocal()),
                @CreatedBy
            );
        END

       

        -- Insert or Update Members
        SELECT
            @MemberId = tmp.[MemberId],
            @Name = tmp.[Name],
            @ContactNumber = tmp.[ContactNumber],
            @Email = tmp.[Email],
            @CountryCode = tmp.[CountryCode],
            @PasswordHash = tmp.[PasswordHash],
            @PasswordSalt = tmp.[PasswordSalt],
            @ConfirmationToken = tmp.[ConfirmationToken],
            @TokenGenerationTime = tmp.[TokenGenerationTime],
            @PasswordRecoveryToken = tmp.[PasswordRecoveryToken],
            @RecoveryTokenTime = tmp.[RecoveryTokenTime],
            @FlatId = tmp.[FlatId],
            @TanentCode = tmp.[TanentCode],
            @IsActive = tmp.[IsActive],
            @CreatedDate = tmp.[CreatedDate],
            @CreatedBy = tmp.[CreatedBy],
            @UpdatedDate = tmp.[UpdatedDate],
            @UpdatedBy = tmp.[UpdatedBy],
            @FailedLoginAttempts = tmp.[FailedLoginAttempts],
            @LastLoginTime = tmp.[LastLoginTime],
            @AccountLockTime = tmp.[AccountLockTime],
            @IsAccountLock = tmp.[IsAccountLock]
        FROM OPENXML(@intPointer, 'Root/Members/Member', 2)
        WITH
        (
            [MemberId] BIGINT,
            [Name] NVARCHAR(100),
            [ContactNumber] NVARCHAR(100),
            [Email] NVARCHAR(100),
            [CountryCode] NVARCHAR(100),
            [PasswordHash] NVARCHAR(250),
            [PasswordSalt] NVARCHAR(100),
            [ConfirmationToken] NVARCHAR(100),
            [TokenGenerationTime] DATETIME,
            [PasswordRecoveryToken] NVARCHAR(100),
            [RecoveryTokenTime] DATETIME,
            [FlatId] BIGINT,
            [TanentCode] NVARCHAR(250),
            [IsActive] BIT,
            [CreatedDate] DATETIME,
            [CreatedBy] BIGINT,
            [UpdatedDate] DATETIME,
            [UpdatedBy] BIGINT,
            [FailedLoginAttempts] INT,
            [LastLoginTime] DATETIME,
            [AccountLockTime] DATETIME,
            [IsAccountLock] BIT
        ) tmp;

        IF EXISTS (SELECT 1 FROM dbo.Members WHERE MemberId = @MemberId)
        BEGIN
            UPDATE dbo.Members
            SET
                Name = @Name,
                ContactNumber = @ContactNumber,
                Email = @Email,
                CountryCode = @CountryCode,
                PasswordHash = @PasswordHash,
                PasswordSalt = @PasswordSalt,
                ConfirmationToken = @ConfirmationToken,
                TokenGenerationTime = @TokenGenerationTime,
                PasswordRecoveryToken = @PasswordRecoveryToken,
                RecoveryTokenTime = @RecoveryTokenTime,
                FlatId = @FlatId,
                TanentCode = @TanentCode,
                IsActive = @IsActive,
                UpdatedDate = (select dbo.GetDateLocal()),
                UpdatedBy = @UpdatedBy,
                FailedLoginAttempts = @FailedLoginAttempts,
                LastLoginTime = @LastLoginTime,
                AccountLockTime = @AccountLockTime,
                IsAccountLock = @IsAccountLock
            WHERE MemberId = @MemberId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.Members
            (
                MemberId,
                Name,
                ContactNumber,
                Email,
                CountryCode,
                PasswordHash,
                PasswordSalt,
                ConfirmationToken,
                TokenGenerationTime,
                PasswordRecoveryToken,
                RecoveryTokenTime,
                FlatId,
                TanentCode,
                IsActive,
                CreatedDate,
                CreatedBy,
                FailedLoginAttempts,
                LastLoginTime,
                AccountLockTime,
                IsAccountLock
            )
            VALUES
            (
                @MemberId,
                @Name,
                @ContactNumber,
                @Email,
                @CountryCode,
                @PasswordHash,
                @PasswordSalt,
                @ConfirmationToken,
                @TokenGenerationTime,
                @PasswordRecoveryToken,
                @RecoveryTokenTime,
                @FlatId,
                @TanentCode,
                @IsActive,
                (select dbo.GetDateLocal()),
                @CreatedBy,
                @FailedLoginAttempts,
                @LastLoginTime,
                @AccountLockTime,
                @IsAccountLock
            );
        END

    
   

        -- Insert or Update MemberRoles
        SELECT
            @MemberRoleId = tmp.[MemberRoleId],
            @MemberId = tmp.[MemberId],
            @RoleMasterId = tmp.[RoleMasterId],
            @TanentCode = tmp.[TanentCode],
            @IsActive = tmp.[IsActive],
            @CreatedDate = tmp.[CreatedDate],
            @CreatedBy = tmp.[CreatedBy],
            @UpdatedDate = tmp.[UpdatedDate],
            @UpdatedBy = tmp.[UpdatedBy]
        FROM OPENXML(@intPointer, 'Root/MemberRoles/MemberRole', 2)
        WITH
        (
            [MemberRoleId] BIGINT,
            [MemberId] BIGINT,
            [RoleMasterId] BIGINT,
            [TanentCode] NVARCHAR(250),
            [IsActive] BIT,
            [CreatedDate] DATETIME,
            [CreatedBy] BIGINT,
            [UpdatedDate] DATETIME,
            [UpdatedBy] BIGINT
        ) tmp;

        IF EXISTS (SELECT 1 FROM dbo.MemberRoles WHERE MemberRoleId = @MemberRoleId)
        BEGIN
            UPDATE dbo.MemberRoles
            SET
                MemberId = @MemberId,
                RoleMasterId = @RoleMasterId,
                TanentCode = @TanentCode,
                IsActive = @IsActive,
                UpdatedDate = (select dbo.GetDateLocal()),
                UpdatedBy = @UpdatedBy
            WHERE MemberRoleId = @MemberRoleId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.MemberRoles
            (
                MemberId,
                RoleMasterId,
                TanentCode,
                IsActive,
                CreatedDate,
                CreatedBy
            )
            VALUES
            (
                @MemberId,
                @RoleMasterId,
                @TanentCode,
                @IsActive,
                (select dbo.GetDateLocal()),
                @CreatedBy
            );
        END

        EXEC sp_xml_removedocument @intPointer;

    END TRY
    BEGIN CATCH
        SELECT @ErrMsg = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
        RETURN;
    END CATCH
END