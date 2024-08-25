CREATE TABLE [dbo].[MemberRoles] (
    [MemberRoleId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [MemberId]     BIGINT         NOT NULL,
    [RoleMasterId] BIGINT         NOT NULL,
    [TanentCode]   NVARCHAR (250) NOT NULL,
    [IsActive]     BIT            NULL,
    [CreatedDate]  DATETIME       NOT NULL,
    [CreatedBy]    BIGINT         NULL,
    [UpdatedDate]  DATETIME       NULL,
    [UpdatedBy]    BIGINT         NULL,
    CONSTRAINT [PK_MemberRoles] PRIMARY KEY CLUSTERED ([MemberRoleId] ASC),
    CONSTRAINT [FK_MemberRoles_Members] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([MemberId]),
    CONSTRAINT [FK_MemberRoles_RoleMaster] FOREIGN KEY ([RoleMasterId]) REFERENCES [dbo].[RoleMaster] ([RoleMasterId])
);

