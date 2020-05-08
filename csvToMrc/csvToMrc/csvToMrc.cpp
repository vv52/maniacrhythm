// csvToMrc.cpp : This program converts Excel *.csv output to MRC format
//				  for use with ManiacRhythm alpha v0.1 and saves as *.txt

#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
using namespace std;

string promptUserInputFile();
string promptUserOutputFile();
vector<string> readCsv(string& file);
vector<string> formatData(vector<string>& rawData);
void writeTxt(vector<string>& convertedChart, string& file);

int main()
{
	string csvFile;
	string txtFile;
	vector<string> rawData;
	vector<string> formattedData;

	// Welcome user to program
	cout << "csvToMrc : Excel to MRC Chart Converter v0.1"
		<< endl << "--------------------------------------------"
		<< endl << "For use with ManiacRhythm v0.1"
		<< endl << "Written by vv52" << endl << endl;

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
	cout << "Writing to " << txtFile << "..." << endl;
	writeTxt(formattedData, txtFile);

	// Let user know when operation is complete
	cout << "Conversion complete." << endl;
	return 0;
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

vector<string> readCsv(string& file)
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

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu