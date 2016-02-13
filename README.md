# CSharp_FinancialReport_Evaluation

Name:
	c#-FinancialReport-Evalution

Target:
	IS,BS,CF
	IncomeStatement, BalanceSheet, CashFlow from Yahoo

Utility Class:

	Class NewReport
		Method farmReport 
			farm IS,BS,CF and save whatever we get √
		Method parseRawReport 
			delete invalid reports, parse reports to readable format

	class EvaluateReport
		CalculateRatio
			load parsed file to report instances and save calculated ratios
		AnalysisPerformance
			read calculated ratios and evaluate tickers
		

Information Class:
	Ticker
	IS,BS,CF:Report(make it as superclass) 

Files:
	
todo:
0. use more native types/APIS
1. reduce hard coded path/file names
2. valid input and return values, handle corner cases

todo-future:
1. multithread
2. XBRL, python
