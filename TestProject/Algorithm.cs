using Kusto.Language;
using Kusto.Language.Syntax;
namespace TestProject
{
    public class Algorithm
    {
        //0
        //wrapper function-call to the next steps{

        //1 call Validation
        //2 call PassQueryFindCustomerData
        //call replace

        //}

        //1
        /// <summary>
        /// Validation checks to the query
        /// </summary>
        /// <param name="query">a Kusto query</param>
        /// <returns>true if the query is correct and false if not</returns>
        private static bool Validation(string query)
        {
            if (query == null)
                return false;
            var diagnostics = KustoCode.ParseAndAnalyze(query).GetDiagnostics();
            return diagnostics.Count == 0;
        }

        //2
        /// <summary>
        /// pass the query, find the sensitve code.
        /// </summary>
        /// <param name="query">a Kusto query</param>
        /// <param name="CustomerDataWords">list of all customer data had found</param>
        /// <param name="code">Convert the query in order to prase it</param>
        /// <returns>list of all customer data had found</returns>
        // now it's public in order to run it but it will be private
        public static List<string> PassQueryFindCustomerData(string query)
        {
            List<string> CustomerDataWords = new List<string>();
            var code = KustoCode.Parse(query);
            SyntaxElement.WalkNodes(code.Syntax,
                n =>
                {
                    {
                        switch (n.Kind)
                        {
                            //Sensitive Operators-might contain Customer Data
                            //each Node operator represents root of tree, the first Descendant is the Customer Data word.

                            case SyntaxKind.ExtendOperator:
                                CustomerDataWords.Add(n.GetFirstDescendant<NameDeclaration>().ToString());
                                break;
                            case SyntaxKind.LetStatement:
                                CustomerDataWords.Add(n.GetFirstDescendant<NameDeclaration>().ToString());
                                break;
                            case SyntaxKind.LookupOperator:
                                CustomerDataWords.Add(n.GetFirstDescendant<NameDeclaration>().ToString());
                                break;
                            case SyntaxKind.SummarizeOperator:
                                CustomerDataWords.Add(n.GetFirstDescendant<NameDeclaration>().ToString());
                                break;
                            case SyntaxKind.ProjectRenameOperator:
                                CustomerDataWords.Add(n.GetFirstDescendant<NameDeclaration>().ToString());
                                break;
                            case SyntaxKind.AsOperator:
                                CustomerDataWords.Add(n.GetFirstDescendant<NameDeclaration>().ToString());
                                break;


                            //Sensitive Parmeters-themselvs Customer Data word.

                            case SyntaxKind.NamedParameter:
                                CustomerDataWords.Add(n.ToString());
                                break;
                            case SyntaxKind.StringLiteralExpression:
                                CustomerDataWords.Add(n.ToString());
                                break;
                            case SyntaxKind.FunctionCallExpression:
                                //check if a function declaration called a customer data
                                CustomerDataWords.Add(n.ToString());
                                break;
                            case SyntaxKind.SkippedTokens:
                                //to check 
                                break;
                        }
                    }
                });
            return CustomerDataWords;
        }
        //3
        //Replace the customer data words 

        //4
        //return the clean query
    }
}
