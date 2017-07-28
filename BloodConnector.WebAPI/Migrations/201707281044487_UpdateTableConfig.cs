namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attachment", "FileguId", c => c.String(maxLength: 256));
            AlterColumn("dbo.Attachment", "Type", c => c.String(maxLength: 128));
            AlterColumn("dbo.Attachment", "FileName", c => c.String(maxLength: 256));
            AlterColumn("dbo.Attachment", "ContentType", c => c.String(maxLength: 128));
            AlterColumn("dbo.BloodGroup", "Symbole", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BloodGroup", "Symbole", c => c.String());
            AlterColumn("dbo.Attachment", "ContentType", c => c.String());
            AlterColumn("dbo.Attachment", "FileName", c => c.String());
            AlterColumn("dbo.Attachment", "Type", c => c.String());
            AlterColumn("dbo.Attachment", "FileguId", c => c.String());
        }
    }
}
