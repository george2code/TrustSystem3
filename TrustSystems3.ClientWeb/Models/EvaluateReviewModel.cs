using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrustSystems3.ClientWeb.Utils;

namespace TrustSystems3.ClientWeb.Models
{
    public class EvaluateReviewModel
    {
        [Range(1, 5, ErrorMessage = "Рейтинг должен быть от 1 до 5")]
        public int Rating { get; set; }

        public string OrderId { get; set; }

        [DisplayName("Заголовок Вашего отзыва")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string ReviewTitle { get; set; }

        [DisplayName("Ваш отзыв")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string ReviewDescription { get; set; }

//        [BooleanRequired(ErrorMessage = "Перед отправлением отзыва примите Условия Соглашения")]
//        public bool Conditions { get; set; }

        public Companies Company { get; set; }
    }
}