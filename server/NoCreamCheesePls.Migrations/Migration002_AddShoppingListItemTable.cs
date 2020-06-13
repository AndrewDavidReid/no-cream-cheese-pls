using FluentMigrator;

namespace NoCreamCheesePls.Data.Migrations
{
  [Migration(2)]
  public class Migration002AddShoppingListItemTable : Migration
  {
    public override void Up()
    {
      Create.Table("shopping_list_item")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("shopping_list_id").AsGuid().NotNullable().ForeignKey("shopping_list", "id")
        .WithColumn("text").AsString(255).NotNullable()
        .WithColumn("completed").AsBoolean().NotNullable()
        .WithColumn("created_on").AsDateTime().NotNullable()
        .WithColumn("last_updated_on").AsDateTime().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("shopping_list_item");
    }
  }
}
