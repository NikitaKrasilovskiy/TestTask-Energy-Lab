using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _mouseSprite;
    [SerializeField] private Image _mouse;
    [SerializeField] private GameObject _miniText;
    [SerializeField] private GameObject[] _textMiniMouse;
    [SerializeField] private CameraRotate _cameraRotate;
    [SerializeField] private GameObject _object;
    [SerializeField] private RectTransform _scrollDetails;
    [SerializeField] private RectTransform _panelText;
    [SerializeField] private Transform _panelMenu;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonDemo;
    [SerializeField] private Button _buttonExit;
    [SerializeField] private GameObject[] _textMouse;
    [SerializeField] private GameObject[] _details;
    [SerializeField] private TextMeshProUGUI _textDescription;

    private Tween activAnim;

    private void Awake()
    {
        _buttonStart.onClick.AddListener(delegate { Play(); CloseDemo(); });
        _buttonDemo.onClick.AddListener(delegate { Demo();});
        _buttonExit.onClick.AddListener(delegate { Exit(); });
    }

    private void Play()
    {
        DOTween.Sequence()
            .Join(_panelMenu.transform.DOScale(0, 1).SetEase(Ease.InBack))
            .Join(_object.transform.DOScale(0.01f, 3).SetEase(Ease.InOutSine))
            .Join(_buttonExit.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-150, -70, 0), 1).SetEase(Ease.InOutSine))
            .Join(_scrollDetails.DOAnchorPos(new Vector3(150, 0, 0), 1).SetEase(Ease.InOutSine))
            .Join(_panelText.DOAnchorPos(new Vector3(-200, 0, 0), 2).SetEase(Ease.InOutSine))
            .AppendCallback(delegate { _panelMenu.gameObject.SetActive(false); _miniText.SetActive(true); });
    }

    private void Demo()
    {
        _buttonDemo.onClick.RemoveAllListeners();
        _buttonDemo.onClick.AddListener(delegate { CloseDemo(); });
        _buttonDemo.GetComponentInChildren<TextMeshProUGUI>().text = "Скрыть управление";

        activAnim = DOTween.Sequence()
            .Join(_mouse.gameObject.transform.DOScale(0.8f, 1).SetEase(Ease.InOutSine))
            .Join(_mouse.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-873, 130, 0), 1).SetEase(Ease.InOutSine))
            .AppendCallback(delegate { _mouse.sprite = _mouseSprite[1]; })
            .Append(_textMouse[0].transform.DOScale(1, 1).SetEase(Ease.InOutSine))
            .AppendInterval(1)
            .AppendCallback(delegate { _mouse.sprite = _mouseSprite[2]; })
            .Append(_textMouse[1].transform.DOScale(1, 1).SetEase(Ease.InOutSine))
            .AppendInterval(1)
            .AppendCallback(delegate { _mouse.sprite = _mouseSprite[3]; })
            .Append(_textMouse[2].transform.DOScale(1, 1).SetEase(Ease.InOutSine))
            .AppendInterval(1)
            .AppendCallback(delegate { _mouse.sprite = _mouseSprite[0]; });
    }

    private void CloseDemo()
    {
        activAnim.Kill(false);
        _mouse.sprite = _mouseSprite[0];

        _buttonDemo.GetComponentInChildren<TextMeshProUGUI>().text = "Показать управление";

        foreach (var item in _textMouse)
        { item.transform.DOScale(0, 1).SetEase(Ease.InOutSine); }

        _mouse.gameObject.transform.DOScale(0.5f, 1).SetEase(Ease.InOutSine);
        _mouse.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-100, 100, 0), 1).SetEase(Ease.InOutSine);
        _buttonDemo.onClick.RemoveAllListeners();
        _buttonDemo.onClick.AddListener(delegate { Demo(); });
    }

    public void ViewDetails(int buttonID)
    {
        TextDescription(buttonID);

        foreach (var item in _details)
        { item.SetActive(true); }

        if (buttonID == -1) return;

        for (int i = 0; i < _details.Length; i++)
        {
            if (buttonID == i) continue;

            _details[i].SetActive(false);
        }
    }

    private void TextDescription(int id)
    {
        switch (id)
        {
            case -1: _textDescription.text = "Выключатель автоматический дифференциального тока"; break;
            case 0: _textDescription.text = "Верхняя крышка защищает и держит конструкцию"; break;
            case 1: _textDescription.text = "Винты закрепляют контакты входного и выходного тока"; break;
            case 2: _textDescription.text = "Зажимы для удержания контактов"; break;
            case 3: _textDescription.text = "Заклепки описание данной детали"; break;
            case 4: _textDescription.text = "Дугогасительная камера, дуга при попадании в эту камеру разбивается на части и теряет свою энергию"; break;
            case 5: _textDescription.text = "Клемы задние описание данной детали"; break;
            case 6: _textDescription.text = "Клемы передние описание данной детали"; break;
            case 7: _textDescription.text = "Кнопка описание данной детали"; break;
            case 8: _textDescription.text = "Кнопка включения описание данной детали"; break;
            case 9: _textDescription.text = "Кнопка индикатора описание данной детали"; break;
            case 10: _textDescription.text = "Коннектор описание данной детали"; break;
            case 11: _textDescription.text = "Крышка делитель описание данной детали"; break;
            case 12: _textDescription.text = "Нижняя кшыка защищает и держит конструкцию"; break;
            case 13: _textDescription.text = "Реле описание данной детали"; break;
            case 14: _textDescription.text = "Реле блок описание данной детали"; break;
            case 15: _textDescription.text = "Трансформатор и обвязка описание данной детали"; break;
            default: break;
        }
    }

    private void Exit()
    { SceneManager.LoadScene(0); }
}