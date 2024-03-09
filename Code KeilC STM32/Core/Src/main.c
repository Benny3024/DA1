/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2023 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */
/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "math.h"

uint8_t txBuffer[13];
uint8_t RxBuffer[13];
uint16_t SetValue;
uint16_t Target ;

//********************* SET PWM *************
volatile uint8_t duty;
//********************* READ RPM ENCODER ****
int32_t encoder_cnt;
int32_t encoder_cnt_pre ;
volatile int32_t read_rpm ;
volatile int32_t rpm;
//********************* PID *****************
float Kp ;
float Ki ;
float Kd ;
volatile float u  ;
float error ;
float integral ; 
float derivative; 
float pre_error;

volatile float dt ;
//********************** Output *************
volatile float output_rpm;
volatile float output_pos;
int dir;
//********************* Count Encoder *****************
int32_t pos_cnt;
volatile int32_t read_pos;
volatile int32_t Flag = 0;
//*********************Load************************
uint8_t load=0;
uint16_t val;
//******************READ ENCODER*************************
int32_t rpmPrev ;
int32_t rpmfilter ;
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim2;

UART_HandleTypeDef huart1;

/* USER CODE BEGIN PV */

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM2_Init(void);
static void MX_USART1_UART_Init(void);
/* USER CODE BEGIN PFP */

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

long map(long x, long in_min, long in_max, long out_min, long out_max) {
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void pwm_set_duty(uint8_t duty)//0-100
{
	uint16_t ccr = duty*htim1.Instance->ARR/100;
  htim1.Instance->CCR1 = ccr;
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

int read_rpm_encoder(void)
{

	uint32_t tickstart = HAL_GetTick();

  while((HAL_GetTick() - tickstart) <= 100)
  {
    encoder_cnt = __HAL_TIM_GetCounter(&htim2); 
  }
		 
	   rpm = (encoder_cnt - encoder_cnt_pre)*1000/(100*22);
		
		 encoder_cnt_pre = encoder_cnt; 
	   rpmfilter = 0.854*rpmfilter + 0.0728*rpm + 0.0728*rpmPrev;
	   rpmPrev = rpm;
		 return rpmfilter;
}

int count_encoder(void)
{
	uint32_t tickstart = HAL_GetTick();
  while((HAL_GetTick() - tickstart) <= 100)
  {
    pos_cnt = __HAL_TIM_GetCounter(&htim2); 
  }
return pos_cnt;
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void pid_control(uint16_t setpoint, int actual)
{
  // Tính sai sai so (error)
  error = (float)(setpoint  - actual)   ;
  // Tính tông tích phân (integral)
  integral+= (float)error*0.1;
  // Tính dao hàm (derivative)
  derivative = (float)(error - pre_error)/0.1; 
  // Tính giá tri diêu khiên (output)
  u = (Kp * error + Ki * integral + Kd * derivative);

  pre_error = error;

}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void transmit_data(int read,int set)
{
	for (int i = 0; i < 3; i++) 
	{
		 txBuffer[i] = RxBuffer[i];
	}
	for (int i = 0; i < 2; i++) 
	{
    txBuffer[i+3] = ((read) >> (i*8))& 0xFF;
  }
		for (int i = 0; i < 2; i++) 
	{
		 txBuffer[i+5] = ((set) >> (i*8))& 0xFF;;
	}
	for (int i = 0; i < 6; i++) 
	{
		 txBuffer[i+7] = RxBuffer[i+7];
	}
		HAL_UART_Transmit(&huart1, txBuffer, 13, HAL_MAX_DELAY);
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void Setmotor(int dir, int pwm)
{
	HAL_TIM_PWM_Start(&htim1,TIM_CHANNEL_1);
	if(dir == 1)
	{
		 HAL_GPIO_WritePin(GPIOB, GPIO_PIN_15, 1);
     HAL_GPIO_WritePin(GPIOB, GPIO_PIN_14, 0);
	}else if(dir == -1)
	{
		HAL_GPIO_WritePin(GPIOB, GPIO_PIN_15, 0);
    HAL_GPIO_WritePin(GPIOB, GPIO_PIN_14, 1);
	}else
	{
		HAL_GPIO_WritePin(GPIOB, GPIO_PIN_15, 0);
    HAL_GPIO_WritePin(GPIOB, GPIO_PIN_14, 0);
	}
	pwm_set_duty(pwm);
}



/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */


  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_TIM1_Init();
  MX_TIM2_Init();
  MX_USART1_UART_Init();
  /* USER CODE BEGIN 2 */

HAL_UART_Receive_IT(&huart1, RxBuffer, 13);

  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
		if(load == 0)
		{
			val = 333;
		}
		else
		{
			val = 250;
		}
// VELOCITY *********************************************
		if(Flag ==1)
		{
			read_rpm = read_rpm_encoder();	
      pid_control(SetValue,read_rpm);		
			if(u > val)
			{
				u = val;
			}
      output_rpm  = (int)fabs(u);
		  duty = map(output_rpm ,0,val,0,100);
			if(duty > 100)
			{
				duty = 100;
			}
			if (duty < 10)
			{
				duty = 10;
			}
     // Ðiêu khiên dông co bang PWM
      Setmotor(1, duty);
      transmit_data(read_rpm,SetValue);
		}
// POSITION *****************************************
		if(Flag == 2)
		{
//			int dir =0;
			read_pos  = count_encoder();
	    pid_control(Target, read_pos);
			output_pos = (int)fabs(u);
			//DRIVER
			if(u < 0)
			{
				dir = -1;
				output_pos = 12;
			}
			else if(u > 0)
			{
				dir = 1;
			}
      else{dir =0;}
			
			Setmotor(dir,output_pos);
			transmit_data(read_pos,Target);
		}
//STOP******************************
		if(Flag ==3)
		{
			__HAL_TIM_SetCounter(&htim2, 0);
			HAL_TIM_PWM_Stop(&htim1,TIM_CHANNEL_1);
		 	HAL_TIM_Encoder_Stop(&htim2,TIM_CHANNEL_1 | TIM_CHANNEL_2);
			NVIC_SystemReset();
			break;
		}
 
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL9;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }

  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief TIM1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM1_Init(void)
{

  /* USER CODE BEGIN TIM1_Init 0 */

  /* USER CODE END TIM1_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};
  TIM_BreakDeadTimeConfigTypeDef sBreakDeadTimeConfig = {0};

  /* USER CODE BEGIN TIM1_Init 1 */

  /* USER CODE END TIM1_Init 1 */
  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 143;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 9999;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim1, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCNPolarity = TIM_OCNPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  sConfigOC.OCIdleState = TIM_OCIDLESTATE_RESET;
  sConfigOC.OCNIdleState = TIM_OCNIDLESTATE_RESET;
  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  sBreakDeadTimeConfig.OffStateRunMode = TIM_OSSR_DISABLE;
  sBreakDeadTimeConfig.OffStateIDLEMode = TIM_OSSI_DISABLE;
  sBreakDeadTimeConfig.LockLevel = TIM_LOCKLEVEL_OFF;
  sBreakDeadTimeConfig.DeadTime = 0;
  sBreakDeadTimeConfig.BreakState = TIM_BREAK_DISABLE;
  sBreakDeadTimeConfig.BreakPolarity = TIM_BREAKPOLARITY_HIGH;
  sBreakDeadTimeConfig.AutomaticOutput = TIM_AUTOMATICOUTPUT_DISABLE;
  if (HAL_TIMEx_ConfigBreakDeadTime(&htim1, &sBreakDeadTimeConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM1_Init 2 */

  /* USER CODE END TIM1_Init 2 */
  HAL_TIM_MspPostInit(&htim1);

}

/**
  * @brief TIM2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM2_Init(void)
{

  /* USER CODE BEGIN TIM2_Init 0 */

  /* USER CODE END TIM2_Init 0 */

  TIM_Encoder_InitTypeDef sConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM2_Init 1 */

  /* USER CODE END TIM2_Init 1 */
  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 0;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 65535;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  sConfig.EncoderMode = TIM_ENCODERMODE_TI12;
  sConfig.IC1Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC1Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC1Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC1Filter = 0;
  sConfig.IC2Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC2Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC2Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC2Filter = 0;
  if (HAL_TIM_Encoder_Init(&htim2, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM2_Init 2 */

  /* USER CODE END TIM2_Init 2 */

}

/**
  * @brief USART1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART1_UART_Init(void)
{

  /* USER CODE BEGIN USART1_Init 0 */

  /* USER CODE END USART1_Init 0 */

  /* USER CODE BEGIN USART1_Init 1 */

  /* USER CODE END USART1_Init 1 */
  huart1.Instance = USART1;
  huart1.Init.BaudRate = 9600;
  huart1.Init.WordLength = UART_WORDLENGTH_8B;
  huart1.Init.StopBits = UART_STOPBITS_1;
  huart1.Init.Parity = UART_PARITY_NONE;
  huart1.Init.Mode = UART_MODE_TX_RX;
  huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart1.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART1_Init 2 */

  /* USER CODE END USART1_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOC, GPIO_PIN_13, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOB, GPIO_PIN_14|GPIO_PIN_15, GPIO_PIN_RESET);

  /*Configure GPIO pin : PC13 */
  GPIO_InitStruct.Pin = GPIO_PIN_13;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

  /*Configure GPIO pins : PB14 PB15 */
  GPIO_InitStruct.Pin = GPIO_PIN_14|GPIO_PIN_15;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

}

/* USER CODE BEGIN 4 */
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
	if(huart->Instance == huart1.Instance)
	{
		HAL_TIM_Encoder_Start(&htim2,TIM_CHANNEL_1 | TIM_CHANNEL_2);
		if(RxBuffer[0] == 'V' && RxBuffer[1] == 'E' &&  RxBuffer[2] =='L')
		{
			load = (uint8_t)(RxBuffer[3]);
			SetValue =((uint16_t)RxBuffer[5] << 8) | RxBuffer[6]; // k?t h?p hai byte thành m?t s? 16-bit
      Kp = (float)(((uint16_t)RxBuffer[7] << 8) | RxBuffer[8])/10000;
			Ki = (float)(((uint16_t)RxBuffer[9] << 8) | RxBuffer[10])/10000;
			Kd = (float)(((uint16_t)RxBuffer[11] << 8) | RxBuffer[12])/10000;

			Flag =1;
	  }else if(RxBuffer[0] == 'P' && RxBuffer[1] == 'O' &&  RxBuffer[2] == 'S')
		{
			load = (uint8_t)(RxBuffer[3]);
			Target = ((uint16_t)RxBuffer[5] << 8) | RxBuffer[6];
			Kp = (float)(((uint16_t)RxBuffer[7] << 8) | RxBuffer[8])/10000;
			Ki = (float)(((uint16_t)RxBuffer[9] << 8) | RxBuffer[10])/10000;
			Kd = (float)(((uint16_t)RxBuffer[11] << 8) | RxBuffer[12])/10000;
			Flag = 2;	
		}
		else 
		{
		Flag = 3;
		}
		
		HAL_UART_Receive_IT(&huart1, RxBuffer, 13);	
	}
}
/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  __disable_irq();
  while (1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

