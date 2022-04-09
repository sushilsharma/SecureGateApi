using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable, XmlRoot(ElementName = "AppNotifications", IsNullable = false)]
    public class AppNotificationsDTO
    {
        public AppNotificationsDTO()
        {
            AppNotificationsList = new List<AppNotificationsDTO>();
        }

        [XmlElement(ElementName = "AppNotificationsList")]
        public List<AppNotificationsDTO> AppNotificationsList { get; set; }

        public string eventCode { get; set; }

        public string title { get; set; }

        public string notId { get; set; }

        public string message { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
