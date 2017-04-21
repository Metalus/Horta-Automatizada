using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortaAutomatizada.Modules
{

    public enum PacketType : byte
    {
        CT, // Current Time - Epoch
        ST, // Start Task Time - Epoch
        ET, // End Task Time - Epoch

        LW, // Limit Water - litros
        WU, // Water Use
        TA, // Volume Tanque

        LU, // Luz
        UA, // Umidade Ar
        US, // Umidade Solo
        TP, // Temperatura

        TM, // Temperatura Máxima

        UM, // Umidade Ar Máxima
        UI, // Umidade Ar Mínima

        SM, // Umidade Solo Máxima
        SI  // Umidade Solo Mínima 
    }
}
