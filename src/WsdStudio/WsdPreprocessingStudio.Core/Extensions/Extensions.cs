using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WsdPreprocessingStudio.Core.Extensions
{
    public static class Extensions
    {
        public static int IndexOf<T>(this T[] array, T element)
        {
            return Array.IndexOf(array, element);
        }

        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else if (control.IsHandleCreated)
                action();
            else
                throw new Exception("Handle is not created.");
        }

        public static IList<T> ShiftLeft<T>(this IList<T> list, T newElement = default(T))
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                list[i] = list[i + 1];
            }

            list[list.Count - 1] = newElement;

            return list;
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var random = new Random();

            for (var i = 0; i < list.Count - 1; i++)
            {
                var swapIndex = random.Next(i + 1, list.Count - 1);
                var temp = list[i];

                list[i] = list[swapIndex];
                list[swapIndex] = temp;
            }

            return list;
        }

        public static bool IsEmpty<T>(this IList<T> list, T emptyElement = default(T))
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                if (!Equals(list[i], emptyElement))
                    return false;
            }

            return true;
        }

        public static void SwapIndices<T>(this IList<T> list, int index1, int index2)
        {
            var temp = list[index1];

            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static bool SequenceCompare<T>(
            this IList<T> list, IList<T> list2, Func<T, T, bool> comparator)
        {
            if (list.Count != list2.Count)
                return false;
            
            for (var i = 0; i < list.Count; i++)
            {
                if (!comparator(list[i], list2[i]))
                    return false;
            }

            return true;
        }
    }
}