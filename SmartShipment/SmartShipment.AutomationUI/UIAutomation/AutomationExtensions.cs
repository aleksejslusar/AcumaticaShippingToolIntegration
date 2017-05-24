using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation;

namespace SmartShipment.AutomationUI.UIAutomation
{
    public static class AutomationExtensions
    {
        public static void EnsureElementIsScrolledIntoView(this AutomationElement element)
        {
            if (!element.Current.IsOffscreen)
            {
                return;
            }

            if (!(bool)element.GetCurrentPropertyValue(AutomationElement.IsScrollItemPatternAvailableProperty))
            {
                return;
            }

            var scrollItemPattern = element.GetScrollItemPattern();
            scrollItemPattern.ScrollIntoView();
        }

        public static AutomationElement FindDescendentByConditionPath(this AutomationElement element, IEnumerable<Condition> conditionPath)
        {
            var conditions = conditionPath as Condition[] ?? conditionPath.ToArray();
            if (!conditions.Any())
            {
                return element;
            }

            var result = conditions.Aggregate(
                element,
                (parentElement, nextCondition) => parentElement?.FindChildByCondition(nextCondition));

            return result;
        }

        public static AutomationElement FindDescendentByIdPath(this AutomationElement element, IEnumerable<string> idPath)
        {
            var conditionPath = CreateConditionPathForPropertyValues(AutomationElement.AutomationIdProperty, idPath);

            return FindDescendentByConditionPath(element, conditionPath);
        }

        public static AutomationElement FindDescendentByNamePath(this AutomationElement element, IEnumerable<string> namePath)
        {
            var conditionPath = CreateConditionPathForPropertyValues(AutomationElement.NameProperty, namePath);

            return FindDescendentByConditionPath(element, conditionPath);
        }

        public static IEnumerable<Condition> CreateConditionPathForPropertyValues(AutomationProperty property, IEnumerable<object> values)
        {
            var conditions = values.Select(value => new PropertyCondition(property, value));

            return conditions;
        }
        /// <summary>
        /// Finds the first child of the element that has a descendant matching the condition path.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="conditionPath">The condition path.</param>
        /// <returns></returns>
        public static AutomationElement FindFirstChildHavingDescendantWhere(this AutomationElement element, IEnumerable<Condition> conditionPath)
        {
            return
                element.FindFirstChildHavingDescendantWhere(
                    child => child.FindDescendentByConditionPath(conditionPath) != null);
        }

        /// <summary>
        /// Finds the first child of the element that has a descendant matching the condition path.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static AutomationElement FindFirstChildHavingDescendantWhere(this AutomationElement element, Func<AutomationElement, bool> condition)
        {
            var children = element.FindAll(TreeScope.Children, Condition.TrueCondition);

            return children.Cast<AutomationElement>().FirstOrDefault(condition);
        }

        public static AutomationElement FindChildById(this AutomationElement element, string automationId)
        {
            var result = element.FindChildByCondition(
                new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));

            return result;
        }

        public static AutomationElement FindChildByName(this AutomationElement element, string name)
        {
            var result = element.FindChildByCondition(
                new PropertyCondition(AutomationElement.NameProperty, name));

            return result;
        }

        public static AutomationElement FindChildByClass(this AutomationElement element, string className)
        {
            var result = element.FindChildByCondition(
                new PropertyCondition(AutomationElement.ClassNameProperty, className));

            return result;
        }

        public static AutomationElement FindChildByProcessId(this AutomationElement element, int processId)
        {
            var result = element.FindChildByCondition(
                new PropertyCondition(AutomationElement.ProcessIdProperty, processId));

            return result;
        }

        public static AutomationElement FindChildByControlType(this AutomationElement element, ControlType controlType)
        {
            var result = element.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, controlType));

            return result;
        }

        public static AutomationElement FindChildByCondition(this AutomationElement element, Condition condition)
        {
            var result = element.FindFirst(
                TreeScope.Children,
                condition);

            return result;
        }

        /// <summary>
        /// Finds the child text block of an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static AutomationElement FindChildTextBlock(this AutomationElement element)
        {
            var child = TreeWalker.RawViewWalker.GetFirstChild(element);

            if (child != null && Equals(child.Current.ControlType, ControlType.Text))
            {
                return child;
            }

            return null;
        }

        public static Point ToDrawingPoint(this Point point)
        {
            return new Point((int)point.X, (int)point.Y);
        }
    }
}