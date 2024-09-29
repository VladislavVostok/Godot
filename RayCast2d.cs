using Godot;
using System;

public partial class SelectableObject2D : Area2D
{
	public override void _Ready()
	{
		Connect("mouse_entered", new Callable(this, nameof(MouseEntered)));
		Connect("mouse_exited", new Callable(this, nameof(MouseExited)));
	}

	private void OnMouseEntered()
	{
		// Изменяем цвет объекта при наведении
		Modulate = new Color(1, 1, 0); // Например, изменяем цвет на жёлтый
	}

	private void OnMouseExited()
	{
		// Возвращаем исходный цвет объекта
		Modulate = new Color(1, 1, 1); // Возвращаем исходный цвет
	}
}
