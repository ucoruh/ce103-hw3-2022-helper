using System;
using System.Collections.Generic;

namespace LibraryManagement
{
    public class Book
    {
        #region Public Constants
        public const int ID_LENGTH = 4;
        
        public const int TITLE_MAX_LENGTH = 100;
        
        public const int DESCRIPTION_MAX_LENGTH = 300;
        
        public const int AUTHORS_MAX_COUNT = 5;
        public const int AUTHORS_NAME_MAX_LENGTH = 100;
        
        public const int CATEGORY_MAX_COUNT = 5;
        public const int CATEGORY_NAME_MAX_LENGTH = 100;

        public const int BOOK_DATA_BLOCK_SIZE = ID_LENGTH +
                                                TITLE_MAX_LENGTH +
                                                DESCRIPTION_MAX_LENGTH +
                                                (AUTHORS_MAX_COUNT * AUTHORS_NAME_MAX_LENGTH) +
                                                (CATEGORY_MAX_COUNT * CATEGORY_NAME_MAX_LENGTH);
        #endregion

        #region Private Fields
        private int _id;
        private string _title;
        private string _description;
        private List<string> _authors;
        private List<string> _categories;
        #endregion

        #region Public Properties
        public int Id { get { return _id; } set { _id = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public List<string> Authors { get { return _authors; } set { _authors = value; } }
        public List<string> Categories { get { return _categories; } set { _categories = value; } }
        #endregion

        #region Constructors
        public Book()
        {
            _authors = new List<string>();
            _categories = new List<string>();
        }
        #endregion

        #region Utility Methods
        public static byte[] BookToByteArrayBlock(Book book)
        {
            int index = 0;

            byte[] dataBuffer = new byte[BOOK_DATA_BLOCK_SIZE];

            #region copy book id
            byte[] idBytes = ConversionUtility.IntegerToByteArray(book.Id);
            Array.Copy(idBytes, 0, dataBuffer, index, idBytes.Length);
            index += Book.ID_LENGTH;
            #endregion

            #region copy book title
            byte[] titleBytes = ConversionUtility.StringToByteArray(book.Title);
            Array.Copy(titleBytes, 0, dataBuffer, index, titleBytes.Length);
            index += Book.TITLE_MAX_LENGTH;
            #endregion

            #region copy book description
            byte[] descBytes = ConversionUtility.StringToByteArray(book.Description);
            Array.Copy(descBytes, 0, dataBuffer, index, descBytes.Length);
            index += Book.DESCRIPTION_MAX_LENGTH;
            #endregion

            #region copy book authors
            byte[] authorBytes = ConversionUtility.StringListToByteArray(book.Authors,
                                                                            Book.AUTHORS_MAX_COUNT,
                                                                            Book.AUTHORS_NAME_MAX_LENGTH);
            Array.Copy(authorBytes, 0, dataBuffer, index, authorBytes.Length);
            index += authorBytes.Length; //Here we can use also Book.AUTHORS_MAX_COUNT * Book.AUTHORS_NAME_MAX_LENGTH
            #endregion


            #region copy book categories
            byte[] categoryBytes = ConversionUtility.StringListToByteArray(book.Categories,
                                                                            Book.CATEGORY_MAX_COUNT,
                                                                            Book.CATEGORY_NAME_MAX_LENGTH);
            Array.Copy(categoryBytes, 0, dataBuffer, index, categoryBytes.Length);
            index += categoryBytes.Length; //Here we can use also Book.CATEGORY_MAX_COUNT * Book.CATEGORY_NAME_MAX_LENGTH
            #endregion

            if (index != dataBuffer.Length){
                throw new ArgumentException("Index and DataBuffer Size Not Matched");
            }
                
            return dataBuffer;
        }

        public static Book ByteArrayBlockToBook(byte[] byteArray)
        {

            Book book = new Book();

            if(byteArray.Length!=BOOK_DATA_BLOCK_SIZE){
                throw new ArgumentException("Byte Array Size Not Match with Constant Data Block Size");
            }

            int index = 0;

            //byte[] dataBuffer = new byte[BOOK_DATA_BLOCK_SIZE];

            #region copy book id
            byte[] idBytes = new byte[Book.ID_LENGTH];
            Array.Copy(byteArray, index, idBytes, 0, idBytes.Length);
            book.Id = ConversionUtility.ByteArrayToInteger(idBytes);
            
            index += Book.ID_LENGTH;
            #endregion

            #region copy book title
            byte[] titleBytes = new byte[Book.TITLE_MAX_LENGTH];
            Array.Copy(byteArray, index, titleBytes, 0, titleBytes.Length);
            book.Title = ConversionUtility.ByteArrayToString(titleBytes);

            index += Book.TITLE_MAX_LENGTH;
            #endregion

            #region copy book description
            byte[] descBytes = new byte[Book.DESCRIPTION_MAX_LENGTH];
            Array.Copy(byteArray, index, descBytes, 0, descBytes.Length);
            book.Description = ConversionUtility.ByteArrayToString(descBytes);

            index += Book.DESCRIPTION_MAX_LENGTH;
            #endregion

            #region copy book authors

            byte[] authorBytes = new byte[Book.AUTHORS_MAX_COUNT * Book.AUTHORS_NAME_MAX_LENGTH];

            Array.Copy(byteArray, index, authorBytes, 0, authorBytes.Length);

            book.Authors = ConversionUtility.ByteArrayToStringList(authorBytes,
                                                                            Book.AUTHORS_MAX_COUNT,
                                                                            Book.AUTHORS_NAME_MAX_LENGTH);
            
            index += authorBytes.Length;
            #endregion


            #region copy book categories
            byte[] categoryBytes = new byte[Book.CATEGORY_MAX_COUNT * Book.CATEGORY_NAME_MAX_LENGTH];

            Array.Copy(byteArray, index, categoryBytes, 0, categoryBytes.Length);

            book.Categories = ConversionUtility.ByteArrayToStringList(categoryBytes,
                                                                            Book.CATEGORY_MAX_COUNT,
                                                                            Book.CATEGORY_NAME_MAX_LENGTH);

            index += categoryBytes.Length;
            #endregion

            if (index != byteArray.Length)
            {
                throw new ArgumentException("Index and DataBuffer Size Not Matched");
            }

            if (book.Id == 0)
            {
                return null;
            }
            else
            {
                return book;
            }

        }
        #endregion

    }
}
