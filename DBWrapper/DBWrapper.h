// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the DBWRAPPER_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// DBWRAPPER_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DBWRAPPER_EXPORTS
#define DBWRAPPER_API __declspec(dllexport)
#else
#define DBWRAPPER_API __declspec(dllimport)
#endif


#include <windows.h>
#include <sql.h>
#include <sqlext.h>
#include <stdio.h>
#include <conio.h>
#include <tchar.h>
#include <stdlib.h>
#include <sal.h>

/*******************************************/
/* Macro to call ODBC functions and        */
/* report an error on failure.             */
/* Takes handle, handle type, and stmt     */
/*******************************************/

#define TRYODBC(h, ht, x)   {   RETCODE rc = x;\
                                if (rc != SQL_SUCCESS) \
                                { \
                                    HandleDiagnosticRecord (h, ht, rc); \
                                } \
                                if (rc == SQL_ERROR) \
                                { \
                                    fwprintf(stderr, L"Error in " L#x L"\n"); \
                                    goto Exit;  \
                                }  \
                            }

/******************************************/
/* Structure to store information about   */
/* a column.
/******************************************/

typedef struct STR_BINDING {
    SQLSMALLINT         cDisplaySize;           /* size to display  */
    WCHAR* wszBuffer;                           /* display buffer   */
    SQLLEN              indPtr;                 /* size or null     */
    BOOL                fChar;                  /* character col?   */
    struct STR_BINDING* sNext;                 /* linked list      */
} BINDING;


void HandleDiagnosticRecord(SQLHANDLE      hHandle,
    SQLSMALLINT    hType,
    RETCODE        RetCode);

void DisplayResults(HSTMT       hStmt,
    SQLSMALLINT cCols);

void AllocateBindings(HSTMT         hStmt,
    SQLSMALLINT   cCols,
    BINDING** ppBinding,
    SQLSMALLINT* pDisplay);


void DisplayTitles(HSTMT    hStmt,
    DWORD    cDisplaySize,
    BINDING* pBinding);

void SetConsole(DWORD   cDisplaySize,
    BOOL    fInvert);

#define DISPLAY_MAX 50          // Arbitrary limit on column width to display
#define DISPLAY_FORMAT_EXTRA 3  // Per column extra display bytes (| <data> )
#define DISPLAY_FORMAT      L"%c %*.*s "
#define DISPLAY_FORMAT_C    L"%c %-*.*s "
#define NULL_SIZE           6   // <NULL>
#define SQL_QUERY_SIZE      1000 // Max. Num characters for SQL Query passed in.

#define PIPE                L'|'

SHORT   gHeight = 80;       // Users screen height

// This class is exported from the dll
class DBWRAPPER_API CDBWrapper {
public:
	CDBWrapper(void);
	// TODO: add your methods here.
};

extern DBWRAPPER_API int nDBWrapper;

DBWRAPPER_API int getSQLDatabaseData(void);
