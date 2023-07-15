using UnityEngine;
using TMPro;

namespace Dialog
{
    public class Reply
    {
        public Reply(Response[] responses)
        {
            Responses = responses;
            SetUpResponses();
        }

        public void SelfDestruct()
        {
            Object.Destroy(Parent);
        }
        public Response[] Responses;

        private GameObject _parent;
        public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(Reply));

        private Card[] _responseCards;
        public Card[] ResponseCards => _responseCards;

        public void SetUpResponses()
        {
            Card[] textCards = new Card[Responses.Length];

            for (int i = 0; i < Responses.Length; i++)
            {
                int fifoI = Responses.Length - i - 1;

                textCards[i] = new Card(nameof(ResponseCards) + i, Parent.transform)
                    .SetSizeAll(new Vector2(7f, 1f))
                    .SetPositionAll(new Vector3(Cam.Io.Camera.aspect * 2, -4 + (fifoI * 1.15f), -1.5f))
                    .SetTextAlignment(TextAlignmentOptions.Right)
                    .AutoSizeFont(true)
                    .SetSprite(Assets.White)
                    .SetSpriteColor(new Color(.15f, .15f, .15f, .65f))
                    .SetFontScale(.6f, .6f)
                    .SetTextString(Responses[i].Text)
                    .SpriteClickable();
            }

            _responseCards = textCards;
        }
    }
}