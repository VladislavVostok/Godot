using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended
{
	[Tool]
	public partial class Region : Area2D
	{
		private bool _ready = false;

		private bool _mouseHighlightEnable = true;
		[Export]
		public bool MouseHighlightEnable
		{
			get => _mouseHighlightEnable;
			set
			{
				_mouseHighlightEnable = value;
				ProcessMouseHighlight();
			}
		}
		public void ProcessMouseHighlight()
		{
			if (_ready)
			{
				if (_mouseHighlightEnable)
				{
					Connect("mouse_entered", new Callable(this, nameof(MouseEntered)));
					Connect("mouse_exited", new Callable(this, nameof(MouseExited)));
				}
				else
				{
					Disconnect("mouse_entered", new Callable(this, nameof(MouseEntered)));
					Disconnect("mouse_exited", new Callable(this, nameof(MouseExited)));
					MouseHighlightPolygon.Visible = false;
				}
			}

		}
		[Export]
		public Color MouseHighlightColor { get; set; } = new Color(2, 2, 2);
		[Export]
		public Texture2D MouseHighlightTexture { get; set; } = null;
		protected Polygon2D MouseHighlightPolygon => GetNode<Polygon2D>("Collision/MouseHighlightPolygon");

		private bool _highlight = true;

		[Export]
		public bool Highlight
		{
			get => _highlight;
			set
			{
				_highlight = value;
				ProcessHighlight();
			}
		}
		public void ProcessHighlight()
		{
			if (_ready)
			{
				HighlightPolygon.Visible = _highlight;
			}
		}

		private Color _highlightColor = new Color(1, 1, 0);

		[Export]
		public Color HighlightColor
		{
			get => _highlightColor;
			set
			{
				_highlightColor = value;
				if (_ready)
				{
					HighlightPolygon.Color = HighlightColor;
				}
			}
		}
		[Export]
		public Texture2D HighlightTexture { get; set; } = null;

		protected Polygon2D HighlightPolygon => GetNode<Polygon2D>("Collision/HighlightPolygon");

		private CollisionPolygon2D _collisionPolygon2d => GetNode<CollisionPolygon2D>("Collision");

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_collisionPolygon2d.AddChild(new Polygon2D { Name = "HighlightPolygon", Polygon = _collisionPolygon2d.Polygon, Color = HighlightColor, Visible = true });
			_collisionPolygon2d.AddChild(new Polygon2D { Name = "MouseHighlightPolygon", Polygon = _collisionPolygon2d.Polygon, Color = MouseHighlightColor, Visible = false });

			_ready = true;
			ProcessHighlight();
			ProcessMouseHighlight();
		}

		public void MouseEntered()
		{
			MouseHighlightPolygon.Visible = true;
		}

		public void MouseExited()
		{
			MouseHighlightPolygon.Visible = false;
		}
	}
}
