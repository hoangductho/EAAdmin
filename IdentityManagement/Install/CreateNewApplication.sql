DECLARE @Appid int
declare @UserID int
Declare @RoleID int
Declare @ObjectID int
declare @OperationID int
declare @permissionID int
insert into Applications(AppName,[Description],URL) values('SSO','des','localhost:8081');
SELECT @Appid = Scope_Identity();

INSERT INTO [FanxiSecurity].[dbo].[user]
           ([username]
           ,[password]
           ,[firstname]
           ,[familyname]
           ,[PasswordFormat]
           ,[PasswordSalt]
           ,[email]
           ,[PasswordQuestion]
           ,[PasswordAnswer]
           ,[IsApproved]
           ,[IsLockedOut]
           ,[CreateDate]
           ,[LastLoginDate]
           ,[LastPasswordChangedDate]
           ,[LastLockoutDate]
           ,[FailedPasswordAttemptCount]
           ,[FailedPasswordAnswerAttemptCount]
           ,[FailedPasswordAttemptWindowStart]
           ,[FailedPasswordAnswerAttemptWindowStart])
     VALUES
           ('SSO_Admin',
           'FCEA920F7412B5DA7BE0CF42B8C93759',
           'FirstName',
           'familyname',
           1,'MD5',
           'duyetnv@fanxipan.vn',
           '',
           '',
           'true',
           'False',
           GETDATE(),
           GETDATE(),
           GETDATE(),
           GETDATE(),
           0,
           0,
           GETDATE(),
           GETDATE()
           )
     
     SELECT @UserID = Scope_Identity();  
     
     insert into App_User(AppID,UserID) values(@Appid,@UserID);
     
     --insert default role - root
     
     INSERT INTO [role] ([AppID] ,[name]) VALUES(@Appid,'Root')
     SELECT @RoleID = Scope_Identity(); 
    INSERT INTO [user_role](roleid ,userid) values(@RoleID,@UserID);
    
    -- insert default Object and Openration
    
    INSERT INTO [FanxiSecurity].[dbo].[object]
           ([AppID]
           ,[name]
           ,[locked])
     VALUES (@Appid,'IdentitySystem','False')
           
     SELECT @ObjectID = Scope_Identity(); 

    INSERT INTO [FanxiSecurity].[dbo].[operation]
           ([AppID]
           ,[name]
           ,[cancreate]
           ,[canread]
           ,[canupdate]
           ,[candelete]
           ,[islocked])
     VALUES (@Appid,'Manage','True','True','True','True','False')
           
        SELECT @OperationID = Scope_Identity();     

     INSERT INTO [FanxiSecurity].[dbo].[permission]
           ([AppID]
           ,[name]
           ,[objectid]
           ,[operationid])
     VALUES (@Appid,'ManageIDSystem',@ObjectID,@OperationID)
     
           SELECT @permissionID = Scope_Identity();   
           
      INSERT INTO [FanxiSecurity].[dbo].[role_permission]
           ([permissionid]
           ,[roleid])
     VALUES (@permissionID,@RoleID)
GO


