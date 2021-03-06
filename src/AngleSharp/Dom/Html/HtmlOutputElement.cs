﻿namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML output element.
    /// </summary>
    sealed class HtmlOutputElement : HtmlFormControlElement, IHtmlOutputElement
    {
        #region Fields

        String _defaultValue;
        String _value;
        SettableTokenList _for;

        #endregion

        #region ctor

        static HtmlOutputElement()
        {
            RegisterCallback<HtmlOutputElement>(AttributeNames.For, (element, value) => element.TryUpdate(element._for, value));
        }

        public HtmlOutputElement(Document owner, String prefix = null)
            : base(owner, TagNames.Output, prefix)
        {
        }

        #endregion

        #region Properties

        public String DefaultValue
        {
            get { return _defaultValue ?? TextContent; }
            set { _defaultValue = value; }
        }

        public override String TextContent
        {
            get { return _value ?? _defaultValue ?? base.TextContent; }
            set { base.TextContent = value; }
        }

        public String Value
        {
            get { return TextContent; }
            set { _value = value; }
        }

        public ISettableTokenList HtmlFor
        {
            get
            { 
                if (_for == null)
                {
                    _for = new SettableTokenList(this.GetOwnAttribute(AttributeNames.For));
                    _for.Changed += value => UpdateAttribute(AttributeNames.For, value);
                }

                return _for; 
            }
        }

        public String Type
        {
            get { return TagNames.Output; }
        }

        #endregion

        #region Helpers

        protected override Boolean CanBeValidated()
        {
            return true;
        }

        internal override void Reset()
        {
            _value = null;
        }

        #endregion
    }
}
