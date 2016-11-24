using System;
using System.Collections.ObjectModel;

namespace nicochecker
{
	/// <summary>
	/// Tags.
	/// </summary>
	public class Tags
	{
		/// <summary>
		/// Tag.
		/// </summary>
		public class Tag
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="T:nicochecker.Tags.Tag"/> class.
			/// </summary>
			/// <param name="name">Name.</param>
			public Tag(string name)
			{
				Name = name;
			}

			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			public string Name { get; }
		}

		private static Tags Instance = new Tags();

		public static Tag DefaultTag = new Tag("全て");

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns>The instance.</returns>
		public static Tags getInstance()
		{
			return Instance;
		}

		public ObservableCollection<Tag> TagList { get; }

		public void AddTag(string name)
		{
			foreach (var tag in TagList)
			{
				if (name.Equals(tag.Name))
				{
					return;
				}
			}

			TagList.Add(new Tag(name));
		}

		public void ClearTags()
		{
			TagList.Clear();
			TagList.Add(DefaultTag);
		}

		private Tags()
		{
			TagList = new ObservableCollection<Tag>();
			TagList.Add(DefaultTag);
		}
	}
}
