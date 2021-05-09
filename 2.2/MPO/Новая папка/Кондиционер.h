#ifndef КОНДИЦИОНЕР_H_HEADER_INCLUDED_9F6E497D
#define КОНДИЦИОНЕР_H_HEADER_INCLUDED_9F6E497D

// фыв
//##ModelId=6050E73402B1
class Кондиционер
{
  public:
    //##ModelId=6050EB0C03D5
    void Включить/Выключить();

    //##ModelId=608F047403B7
    void Изменить режим работы();

  private:
    //##ModelId=6050EA9401CE
    string Модель;
    //##ModelId=6050EAA902D2
    Настройки кондиционера Hастройки;
    //##ModelId=6050ED350061
    int Питание;
};



#endif /* КОНДИЦИОНЕР_H_HEADER_INCLUDED_9F6E497D */
