using Microsoft.Extensions.Options;
using NovelReader.ApplicationService.Helpers;
using NovelReader.ApplicationService.Options;
using System;
using System.Collections.Generic;
using Xunit;

namespace NovelReader.Tests
{
    public class StringManagerTests
    {
        private StringManager _sut;

        public StringManagerTests()
        {
            var splitSettings = Options.Create(new SplitSettings());
            splitSettings.Value.TextStartPoint = "Start Here";
            splitSettings.Value.SubStringSplitOption = "Sub Section";
            splitSettings.Value.TextEndPoint = "End Here";
            splitSettings.Value.SplitOption = " ";
            splitSettings.Value.WordSize = 5;

            _sut = new StringManager(splitSettings);
        }

        [Fact]
        public void RemoveSpecialChars_ShouldRemoveSpecialChars_IfTextIsValid()
        {
            var withSpecialChars = "“Do help me out, Theodore, sir,” or “your excellency”";
            var expected = "Do help me out Theodore sir or your excellency".ToLower().Split(" ");
            var result = _sut.RemoveSpecialChars(withSpecialChars);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WordsCounter_ShouldCountWords_IfTextIsValid()
        {
            var wordsToCount = ("More than once they drunk and danced with him and more than once they had made him drunk on champagne and Madeira").Split(" ");

            var expected = new Dictionary<string, int>();
            expected.Add("drunk", 2);
            expected.Add("danced", 1);
            expected.Add("champagne", 1);
            expected.Add("Madeira", 1);

            var result = _sut.WordsCounter(wordsToCount);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetNovelAsText_ShouldGetText_IfTextIsValid()
        {
            var novelText = "Start Here Do help me out, Theodore, sir, or your excellency End Here";
            var expected = "Do help me out, Theodore, sir, or your excellency";
            var result = _sut.GetNovelAsText(novelText);
            Assert.Equal(expected, result);
        }

    }
}
