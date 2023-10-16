﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class EditableTag : IEditableObject, ICloneable
    {

        struct TagData
        {
            internal int id;
            internal string tag;
            internal float weight;
            internal bool manualEdited;
            internal string translation;
            internal int order;

            public override bool Equals(object obj)
            {

                if (obj != null && obj.GetType() == typeof(TagData))
                {
                    TagData t2 = (TagData)obj;
                    if (t2.id == id && t2.tag == tag && t2.weight == weight && t2.order == order)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        private EditableTagList parent;
        private TagData tagData;
        private TagData backupData;
        private bool inTxn = false;

        //[DisplayName("ImageTags")]
        public string Tag
        {
            get
            {
                return tagData.tag;
            }
            set
            {
                tagData.tag = value;
                OnEditableTagChanged();
            }
        }

        public string Translation
        {
            get
            {
                return tagData.translation;
            }
            set
            {
                tagData.translation = value;
                OnEditableTagChanged();
            }
        }
        
        public bool IsManual
        {
            get
            {
                return tagData.manualEdited;
            }
            set
            {
                tagData.manualEdited = value;
                OnEditableTagChanged();
            }
        }
        
        public float Weight
        {
            get
            {
                return tagData.weight;
            }
            set
            {
                tagData.weight = value;
                OnEditableTagChanged();
            }
        }
        
        public int Order
        {
            get
            {
                return tagData.order;
            }
            set
            {
                tagData.order = value;
                OnEditableTagChanged();
            }
        }

        public int Id
        {
            get
            {
                return tagData.id;
            }
        }

        internal EditableTagList Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }


        public EditableTag(int id, string tag)
        {
            tagData.id = id;
            tagData.tag = tag;
            tagData.weight = 1;
            tagData.translation = "";
            tagData.order = 0;
            tagData.manualEdited = false;
        }

        public EditableTag(int id, string tag, int orderIndex)
        {
            tagData.id = id;
            tagData.tag = tag;
            tagData.weight = 1;
            tagData.translation = "";
            tagData.order = orderIndex;
            tagData.manualEdited = false;
        }

        public EditableTag() { }

        public void BeginEdit()
        {
            if (!inTxn)
            {
                backupData = tagData;
                inTxn = true;
            }
        }

        public void CancelEdit()
        {
            if (inTxn)
            {
                this.tagData = backupData;
                inTxn = false;
            }
        }

        public void EndEdit()
        {
            if (inTxn)
            {
                inTxn = false;
                if (!tagData.Equals(backupData))
                    OnEditableTagChanged();
                backupData = new TagData();

            }
        }

        private void OnEditableTagChanged()
        {
            if (!inTxn && Parent != null)
            {
                Parent.EditableTagChanged(this, true);
            }
            //if (parent != null)
            //{
            //    for(int i=0;i<
            //}

        }

        public EditableTag GetEditableTagFromBackup()
        {
            var tag = new EditableTag();
            tag.tagData = backupData;
            tag.inTxn = false;
            tag.backupData = new TagData();
            tag.parent = parent;
            return tag;
        }

        public object Clone()
        {
            EditableTag tag = new EditableTag(tagData.id, tagData.tag);
            tag.tagData = tagData;
            tag.backupData = backupData;
            //tag.parent = parent;
            return tag;
        }

        public override string ToString()
        {
            string resTag = Tag;
            if (!resTag.Contains("\\(") && resTag.Contains('('))
                resTag = resTag.Replace("(", "\\(");
            if (!resTag.Contains("\\)") && resTag.Contains(')'))
                resTag = resTag.Replace(")", "\\)");
            if (Weight == 1f)
                return resTag;
            else if (Weight == 0f)
                return "";
            else if (Weight > 1f)
            {
                int brCount = CalcBracketsCount(Weight, true);
                if (brCount != 0)
                {
                    return RepeatString("(", (int)brCount) + resTag + RepeatString(")", (int)brCount);
                }
                else
                {
                    return $"({resTag}:{Weight})";
                }
            }
            else
            {
                int brCount = CalcBracketsCount(Weight, false);
                if (brCount != 0)
                {
                    return RepeatString("[", (int)brCount) + resTag + RepeatString("]", (int)brCount);
                }
                else
                {
                    return $"({resTag}:{Weight})";
                }
            }
        }

        private int CalcBracketsCount(float weight, bool positive)
        {
            if (weight == 1 || weight == 0)
                return 0;
            int count = 0;
            float mult = positive ? PromptParser.round_bracket_multiplier : PromptParser.square_bracket_multiplier;
            
            if (positive)
            {
                while (weight > 1)
                {
                    weight /= mult;
                    count++;
                }
            }
            else
            {
                while (weight < 1)
                {
                    weight /= mult;
                    count++;
                }
            }
            if (weight == 1)
                return count;
            else
                return 0;
        }

        private string RepeatString(string text, int count)
        {
            string result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                result += text;
            }
            return result;
        }


    }
}
