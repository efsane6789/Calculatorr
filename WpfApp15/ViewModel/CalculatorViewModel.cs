using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp15.Commend;

namespace WpfApp15.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged 
    {
        
        private string result;
        public string Result
        {
            get { return result; }
            set
            {
                if(result != value)
                {
                    result = value;
                    OnpropertyChanged("Result");
                }
                
            }
        }
        private string currentNumber;
        private string operation;
        private double previousNumber;

        public ICommand NumberButtonCommand { get; }
        public ICommand OperationButtonCommand { get; }
        public ICommand EqualsButtonCommand { get; }

        public CalculatorViewModel()
        {
            Result="0";
            currentNumber="";
            operation="";
            previousNumber=0;

            NumberButtonCommand=new RelayCommand(AddNumber);
            OperationButtonCommand=new RelayCommand(SetOperation);
            EqualsButtonCommand=new RelayCommand(PerformOperation);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void AddNumber(object parameter)
        {
            string pressedNumber=parameter.ToString();
            if (currentNumber=="0")
            {
                currentNumber=pressedNumber;
            }
            else
            {
                currentNumber+=pressedNumber;
            }
            
            Result=currentNumber;
        }
        private void Clear(object parameter)
        {
            currentNumber = "0";
            previousNumber = 0;
            operation="";
            Result=currentNumber;
        }
        private void SetOperation(object parameter)
        {
            operation=parameter.ToString();
            previousNumber=double.Parse(currentNumber);
            currentNumber="";
        }

        private void PerformOperation(object parameter)
        {
            double currentResult = 0;
            double secondNumber=double.Parse(currentNumber);

            switch (operation)
            {
                case "+": currentResult=previousNumber + secondNumber; break;
                case "-": currentResult=previousNumber - secondNumber; break;
                case "*": currentResult=previousNumber * secondNumber; break;
                case "/": currentResult=previousNumber / secondNumber; break;
                    default:
                    break;
                   
            }
            Result=currentResult.ToString();
            currentNumber=currentResult.ToString();
            previousNumber=currentResult;
            operation="";
        }
    }
}
