Update AspNetUserRoles
set RoleId=1 
where UserId=(Select Id from AspNetUsers where Email='arif.rahman2009@gmail.com')