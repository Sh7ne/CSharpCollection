namespace YourProject
{
	public static class PageHelper
	{
		public static MvcHtmlString BindDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
		{
			//var Model = html.ViewData.Model;
			//Func<TModel, TValue> deleg = expression.Compile();
			//var Value = deleg(Model);

			//var htmlString = Convert.ToString(Value);
			//var result = new MvcHtmlString(htmlString);
			//return result;

			if (htmlHelper == null)
			{
				throw new ArgumentNullException(nameof(htmlHelper));
			}

			if (expression == null)
			{
				throw new ArgumentNullException(nameof(expression));
			}

			var a = htmlAttributes;

			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
			string DisplayText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
			string SimpleDisplayText = metadata.SimpleDisplayText;

			if (String.IsNullOrEmpty(DisplayText))
			{
				return MvcHtmlString.Empty;
			}

			TagBuilder div = new TagBuilder("div");
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			div.MergeAttributes(attributes, false);
			div.SetInnerText(SimpleDisplayText);

			TagBuilder hidden = new TagBuilder("input");
			hidden.Attributes.Add("value", SimpleDisplayText);
			hidden.Attributes.Add("name", htmlFieldName);
			hidden.Attributes.Add("type", "hidden");

			var htmlString = div.ToString(TagRenderMode.Normal) + hidden.ToString(TagRenderMode.SelfClosing);


			return MvcHtmlString.Create(htmlString);
		}
	}
}
