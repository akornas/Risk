using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class OnPointerEnter : MonoBehaviour
{
	public UnityEvent OnPointerEnterEvent;
	public UnityEvent OnPointerExitEvent;
	public UnityEvent OnClickEvent;

	[Inject]
	private readonly IEventSystemProvider _eventSystemProvider;

	[SerializeField]
	private Image _image;

	private RectTransform _rectTransform;
	private bool _isPointerOverImage;

	private Texture2D Texture => _image.sprite.texture;

	private bool IsPointerOverImage
	{
		get => _isPointerOverImage;
		set
		{
			if (_isPointerOverImage != value)
			{
				_isPointerOverImage = value;
				InvokeStateChangedEvent();
			}
		}
	}

	private void InvokeStateChangedEvent()
	{
		if (_isPointerOverImage)
		{
			OnPointerEnterEvent?.Invoke();
		}
		else
		{
			OnPointerExitEvent?.Invoke();
		}
	}

	private RectTransform RectTransform
	{
		get
		{
			if (_rectTransform == null)
			{
				_rectTransform = _image.GetComponent<RectTransform>();
			}

			return _rectTransform;
		}
	}

	private void Update()
	{
		IsCursorOverImage();
		CheckPointerClick();
	}

	private void IsCursorOverImage()
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, null, out var mousePosition))
		{
			if (!IsMouseInRect(mousePosition))
			{
				IsPointerOverImage = false;
				return;
			}

			float normalizedX = (mousePosition.x + RectTransform.rect.width / 2) / RectTransform.rect.width;
			float normalizedY = (mousePosition.y + RectTransform.rect.height / 2) / RectTransform.rect.height;

			int pixelX = Mathf.RoundToInt(normalizedX * Texture.width);
			int pixelY = Mathf.RoundToInt(normalizedY * Texture.height);

			IsPointerOverImage = IsOverNonAlphaTexture(pixelX, pixelY);
			return;
		}

		IsPointerOverImage = false;
	}

	private bool IsOverNonAlphaTexture(int pixelX, int pixelY)
	{
		if (pixelX >= 0 && pixelX < Texture.width && pixelY >= 0 && pixelY < Texture.height)
		{
			var pixelColor = Texture.GetPixel(pixelX, pixelY);

			if (pixelColor.a > 0.01f)
			{
				return true;
			}
		}

		return false;
	}

	private bool IsMouseInRect(Vector2 mousePosition)
	{
		return mousePosition.x >= -RectTransform.rect.width / 2 &&
				mousePosition.x <= RectTransform.rect.width / 2 &&
				mousePosition.y >= -RectTransform.rect.height / 2 &&
				mousePosition.y <= RectTransform.rect.height / 2;
	}

	private void CheckPointerClick()
	{
		if (IsPointerOverImage && Input.GetMouseButtonDown(0))
		{
			_eventSystemProvider.SetSelectedGameObject(this.gameObject);
			OnClickEvent?.Invoke();
		}
	}
}
