// csvToMrc.cpp : This program converts Excel *.csv output to MRC format
// v0.2			  for use with ManiacRhythm alpha v0.1 and saves as *.txt
//
// Changelog:
//
// v0.2 - added ability to redo last operation or do another operation
//        without having to restart the app

#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
using namespace std;

string csvFile;
string txtFile;

string promptUserInputFile();
string promptUserOutputFile();
vector<string> readCsv(string file);
vector<string> formatData(vector<string>& rawData);
void writeTxt(vector<string>& convertedChart, string& file);
void runProgramCycle(vector<string> rawData, vector<string> formattedData);
void redoLastProgramCycle(vector<string> rawData, vector<string> formattedData);

int main()
{
	vector<string> rawData;
	vector<string> formattedData;
	char menuSelection = 'A';

	// Welcome user to program
	cout << "csvToMrc : Excel to MRC Chart Converter v0.2"
		<< endl << "--------------------------------------------"
		<< endl << "For use with ManiacRhythm v0.1"
		<< endl << "Written by vv52" << endl << endl;

	runProgramCycle(rawData, formattedData);

	while (toupper(menuSelection) != 'E')
	{
		cout << "[R]edo last operation, [N]ew operation, or [E]xit program? ";
		cin >> menuSelection;
		if (toupper(menuSelection) == 'R')
		{
			redoLastProgramCycle(rawData, formattedData);
		}
		else if (toupper(menuSelection) == 'N')
		{
			runProgramCycle(rawData, formattedData);
		}
		else if (toupper(menuSelection) != 'E')
		{
			cout << "Invalid input, please input \"R\", \"N\", or \"E\"." << endl;
		}
	}

	return 0;
}

void runProgramCycle(vector<string> rawData, vector<string> formattedData)
{
	// Get import and export filenames
	csvFile = promptUserInputFile();
	txtFile = promptUserOutputFile();

	// Read *.csv
	cout << "Reading " << csvFile << "..." << endl;
	rawData = readCsv(csvFile);

	// Format the data as MRC
	cout << "Formatting data..." << endl;
	formattedData = formatData(rawData);

	// Write the data to a file
	cout << "Writing to " << txtFile << "..." << endl << endl;
	writeTxt(formattedData, txtFile);

	// Clean vectors
	rawData.clear();
	formattedData.clear();

	// Let user know when operation is complete
	cout << "Conversion complete." << endl;
}

void redoLastProgramCycle(vector<string> rawData, vector<string> formattedData)
{
	// Read *.csv
	cout << "Reading " << csvFile << "..." << endl;
	rawData = readCsv(csvFile);

	// Format the data as MRC
	cout << "Formatting data..." << endl;
	formattedData = formatData(rawData);

	// Write the data to a file
	cout << "Writing to " << txtFile << "..." << endl << endl;
	writeTxt(formattedData, txtFile);
}

string promptUserInputFile()
{
	string inputFile;

	cout << "Please specify location and full filename of *.csv: ";
	cin >> inputFile;

	return inputFile;
}

string promptUserOutputFile()
{
	string outputFile;

	cout << "Please specify output filename (including *.txt): ";
	cin >> outputFile;

	return outputFile;
}

vector<string> readCsv(string file)
{
	ifstream csvFile;
	string rawData;
	vector<string> data;

	csvFile.open(file);

	while (csvFile.good())
	{
		getline(csvFile, rawData, '\n');
		data.push_back(rawData);
	}

	csvFile.close();

	return data;
}

vector<string> formatData(vector<string>& rawData)
{
	vector<string> separatedData;
	vector<string> formattedData;

	for (int i = 0; i < rawData.size(); i++)
	{
		string temp;
		stringstream iss;

		iss << rawData[i];
		while (iss.good())
		{
			getline(iss, temp, ',');
			separatedData.push_back(temp);
		}
	}

	for (int j = 0; j < separatedData.size(); j++)
	{
		string temp;
		
		temp.append(separatedData[j + 0]);
		temp.append(" ");
		temp.append(separatedData[j + 1]);
		temp.append(" ");
		temp.append(separatedData[j + 2]);
		temp.append(" ");
		temp.append(separatedData[j + 3]);
		temp.append(" ");
		temp.append(separatedData[j + 4]);
		formattedData.push_back(temp);
		j += 4;
	}

	return formattedData;
}

void writeTxt(vector<string>& convertedChart, string& file)
{
	ofstream txtFile;

	txtFile.open(file);

	for (int i = 0; i < convertedChart.size(); i++)
	{
		txtFile << convertedChart[i] << endl;
	}
}