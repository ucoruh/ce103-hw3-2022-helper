using LibraryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Get Executable Running Path and Set Library.Dat File Path to This Location
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string filename = Path.Combine(path, "library.dat");
            #endregion

            #region Testing Purpose Delete Library.Dat File If Exist
            FileUtility.DeleteFile(filename);
            #endregion

            #region Generate Book1 Object
            Book book1 = new Book();
            book1.Id = 5;
            book1.Title = "Demo Title 1";
            book1.Description = "Demo Description 1";
            book1.Authors.Add("Demo Author 1");
            book1.Authors.Add("Demo Author 2");
            book1.Categories.Add("ScienceFiction");
            book1.Categories.Add("Drama");
            #endregion

            #region Generate Book2 Object
            Book book2 = new Book();
            book2.Id = 6;
            book2.Title = "Demo Title 2";
            book2.Description = "Demo Description 2";
            book2.Authors.Add("Demo Author 3");
            book2.Authors.Add("Demo Author 4");
            book2.Categories.Add("ScienceFiction");
            book2.Categories.Add("Drama");
            Book book3 = new Book();
            #endregion

            #region Generate Book3 Object
            book3.Id = 7;
            book3.Title = "Demo Title 3";
            book3.Description = "Demo Description 3";
            book3.Authors.Add("Demo Author 5");
            book3.Authors.Add("Demo Author 6");
            book3.Categories.Add("ScienceFiction");
            book3.Categories.Add("Drama");
            #endregion

            #region Convert Book Objects to Byte Arrays
            byte[] bookBytes1 = Book.BookToByteArrayBlock(book1);
            byte[] bookBytes2 = Book.BookToByteArrayBlock(book2);
            byte[] bookBytes3 = Book.BookToByteArrayBlock(book3);
            #endregion

            #region Testing Purpose Convert First Book Bytes to Hex for Test Function
            //string bookBytesHex = ConversionUtility.ToHex(bookBytes1);
            #endregion

            #region Append Book1-2-3 to File
            FileUtility.AppendBlock(bookBytes1, filename);
            FileUtility.AppendBlock(bookBytes2, filename);
            FileUtility.AppendBlock(bookBytes3, filename);
            #endregion

            #region Read Second Book Record and Convert Byte to Book Object
            byte[] bookWrittenBytes = FileUtility.ReadBlock(2, Book.BOOK_DATA_BLOCK_SIZE, filename);
            Book bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);
            #endregion

            #region Delete Second Book Record and Check it Deleted
            FileUtility.DeleteBlock(2, Book.BOOK_DATA_BLOCK_SIZE, filename);
            bookWrittenBytes = FileUtility.ReadBlock(2, Book.BOOK_DATA_BLOCK_SIZE, filename);
            bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);
            //Book 2 object will be null
            #endregion

            #region Update Deleted Book 2 Record with Book 1 Data and Read Record for Test
            FileUtility.UpdateBlock(bookBytes1, 2, Book.BOOK_DATA_BLOCK_SIZE, filename);
            bookWrittenBytes = FileUtility.ReadBlock(2, Book.BOOK_DATA_BLOCK_SIZE, filename);
            bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);
            #endregion

            #region Update Book 3 Record with Book 2 and Read Data for Test
            FileUtility.UpdateBlock(bookBytes2, 3, Book.BOOK_DATA_BLOCK_SIZE, filename);
            bookWrittenBytes = FileUtility.ReadBlock(3, Book.BOOK_DATA_BLOCK_SIZE, filename);
            bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);
            #endregion

            #region Convert byte block to book object
            Book book2Object = Book.ByteArrayBlockToBook(bookBytes2);
            #endregion

           
        }
    }
}
