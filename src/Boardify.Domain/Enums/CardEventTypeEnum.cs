using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardify.Domain.Enums
{
    public enum CardEventTypeEnum
    {
        MemberAdded,
        MemberRemoved,
        CardMoved,
        DueDateChanged,
        DescriptionUpdated,
        AddedAttachment,
        RemovedAttachment,
        AddedReporter,
        RemovedReporter,
        AddedPriority,
        ModifiedPriority,
        DeletedPriority,
        AddedComment,
        DeletedComment,
        UpdatedComment,
        ModifiedAttachment
    }
}
