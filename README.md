# AI-Testing-Evaluation
A platform to test different AI models on simular or different datasets.

Define the problem<br>
This platform evaluates AI models across different tasks, subjects, and difficulty levels. It supports benchmarking, model comparison, analytics, and optional training dataset generation. Results are stored and analyzed to measure model performance across various domains and capabilities.

Identify main objects<br>
- Model Types
	- Functionalities
   		- Model

- Subject
	- topics
		- Question
			- ExpectedOutput/GroundTruth (In the code I will just name it ExpectedOutput)

- Dataset
	- Question

- Model
	- BenchmarkSession
		- Evaluation
			- Question
			- ModelResponse
			- Score
			- IsCorrect

Relationships\
Model Types 1 - M -> Functionalities 1 - M -> Model\
Subject 1 - M -> topics 1 - M -> Question 1 - 1 -> ExpectedOutput/GroundTruth\
Dataset 1 - M -> Question\
Model 1 - M -> BenchmarkSession 1 - M -> Evaluation <br>

Database Design

ModelType
---------------
PK  Id			INT\
	Name		VARCHAR(100) \
	Description	TEXT <br>
	
Functionality
---------------
PK	 Id		        INT<br>
FK	 ModelTypeId	INT<br>
     Description	TEXT<br>
	   Name		      VARCHAR(100)<br>

Architecture
---------------
PK	Id		INT<br>
	Name		VARCHAR(100)<br>
	Description	TEXT<br>

Model
---------------
PK	Id		INT<br>
FK	FunctionalityId	INT<br>
FK	ArchitectureId	INT<br>
	Name		VARCHAR(100)<br>
	Version		VARCHAR(50)<br>
	Description	TEXT<br>

Subject
---------------
PK	Id		INT<br>
	Name		VARCHAR(100)<br>
	Description	TEXT<br>

Topic
---------------
PK	Id		INT<br>
FK	SubjectId	INT<br>
	Name		VARCHAR(100)<br>
	Description	TEXT<br>


Dataset
---------------
PK	Id		INT <br>
FK	TopicId		INT<br>
	Name 		VARCHAR(100)<br>
	DataType	VARCHAR(50)<br>
	Description	TEXT<br>

Question
---------------
PK	Id			INT<br>
FK	TopicId			INT<br>
FK	DatasetId		INT<br>
	QuestionType		enum<br>
	EstimatedDifficulty	INT<br>
	TestedDifficulty	INT<br>
	Prompt 			TEXT<br>

ExpectedOutput
---------------
PK	Id		INT<br>
FK	QuestionId	INT<br>
	OutputData	JSON<br>

BenchmarkSession
---------------
PK	Id		INT<br>
FK	ModelId		INT<br>
FK	DatasetId	INT<br>
	StartTime	DATETIME<br>
	EndTime		DATETIME<br>
	Status		VARCHAR(50)<br>

EvaluationResult
---------------
PK	Id			INT<br>
FK	BenchmarkSessionId	INT<br>
FK	QuestionId		INT<br>
FK	ExpectedOutputId	INT<br>
	ModelResponse		JSON<br>
	ResponseTimeMS		INT<br>
	Score			DECIMAL(5, 2)<br>
	IsCorrect		BIT<br>
	CreatedAt		DATETIME<br>

Design APIs<br>
Run Benchmark
---------------
POST /api/benchmarksessions<br>
GET /api/benchmarksessions<br>
GET /api/benchmarksessions/{id}<br>
GET /api/benchmarksessions/{id}/results<br>
POST /api/benchmarksessions/{id}/start<br>
POST /api/benchmarksessions/{id}/finish<br>

Submit Model Response
---------------
POST /api/benchmarksessions/{id}/responses<br>

Analytics
---------------
GET /api/models/{id}/analytics/<br>

Model Comparison
---------------
GET /api/models/{id}/analytics/compare?first={id}&second={id}<br>

ModelType
---------------
POST /api/modeltypes<br>
GET /api/modeltypes<br>
GET /api/modeltypes/{id}<br>
DELETE /api/modeltypes/{id}<br>

Functionality
---------------
POST /api/functionalities<br>
GET /api/functionalities<br>
GET /api/functionalities/{id}<br>
DELETE /api/functionalities/{id}<br>

Model
---------------
POST /api/models<br>
GET /api/models<br>
GET /api/models/{id}<br>
DELETE /api/models/{id}<br>
PATCH /api/models/{id}<br>
PUT /api/models/{id}<br>

Architecture
---------------
POST /api/architectures<br>
GET /api/architectures<br>
GET /api/architectures/{id} <br>
DELETE /api/architectures/{id}<br>

Subject
---------------
POST /api/subjects<br>
GET /api/subjects<br>
GET /api/subjects/{id} <br>
DELETE /api/subjects/{id}<br>

Topic
---------------
POST /api/subjects/{id}/topics<br>
GET /api/subjects/{id}/topics<br>
GET /api/subjects/{id}/topics/{id}<br>
GET /api/subjects/{id}/topics/{id}/questions<br>
DELETE /api/subjects/{id}/topics/{id}<br>

Dataset
---------------
POST /api/topics/{id}/datasets<br>
GET /api/topics/{id}/datasets<br>
GET /api/topics/{id}/datasets/{id} <br>
DELETE /api/topics/{id}/datasets/{id}<br>

Question
---------------
POST /api/questions<br>
GET /api/questions<br>
GET /api/questions/{id} <br>
DELETE /api/questions/{id}<br>

ExpectedOutput
---------------
POST /api/questions/{id}/expectedoutputs<br>
GET /api/questions/{id}/expectedoutputs<br>
GET /api/questions/{id}/expectedoutputs/{id} <br>
DELETE /api/questions/{id}/expectedoutputs/{id}<br>

BenchmarkSession
---------------
POST /api/benchmarksessions<br>
GET /api/benchmarksessions<br>
GET /api/benchmarksessions/{id} <br>
DELETE /api/benchmarksessions/{id}<br>

EvaluationResult
---------------
POST /api/evaluationresults<br>
GET /api/evaluationresults<br>
GET /api/evaluationresults/{id} <br>
DELETE /api/evaluationresults/{id}<br>
