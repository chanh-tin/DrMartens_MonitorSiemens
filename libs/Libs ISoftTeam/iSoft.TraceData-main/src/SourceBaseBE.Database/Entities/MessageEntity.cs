using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using iSoft.Database.Entities;
using iSoft.Common.Enums;
using iSoft.Database.Extensions;

namespace SourceBaseBE.Database.Entities
{
  public class MessageEntity : BaseCRUDEntity
  {
    [FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    public string Title { get; set; }


    [FormDataTypeAttributeText(EnumFormDataType.Textarea)]
    public string Content { get; set; }


    [FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    public string? URL { get; set; }


    public bool IsRead { get; set; } = false;


    [FormDataTypeAttributeDatetime(EnumFormDataType.Datetime, false)]
    public DateTime SendDate { get; set; }


    [ForeignKey(nameof(UserEntity))]
    public long? UserId { get; set; }
    public UserEntity? ItemUser { get; set; }

    public MessageEntity Clone()
    {
      return new MessageEntity
      {
        Title = this.Title,
        Content = this.Content,
        URL = this.URL,
        IsRead = this.IsRead,
        SendDate = this.SendDate,
        UserId = this.UserId,
        // TODO: ItemUser.Clone()
      };
    }
  }
}
