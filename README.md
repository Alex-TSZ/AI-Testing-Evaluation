# AI-Testing-Evaluation
A platform to test different AI models on simular or different datasets.

Step 1: define the problem
This platform evaluates AI models across different tasks, subjects, and difficulty levels. It supports benchmarking, model comparison, analytics, and optional training dataset generation. Results are stored and analyzed to measure model performance across various domains and capabilities.

Step 2: Identify main objects
Model Types
- Functionalities
	- Model

Subject
- topics
	- Question
		- ExpectedOutput/GroundTruth (In the code I will just name it ExpectedOutput)

Dataset
- Question

Model
- BenchmarkSession
	- Evaluation
		- Question
		- ModelResponse
		- Score
		- IsCorrect

Step 3: Draw Relationships
Model Types 1 - M -> Functionalities 1 - M -> Model
Subject 1 - M -> topics 1 - M -> Question 1 - 1 -> ExpectedOutput/GroundTruth
Dataset 1 - M -> Question
Model 1 - M -> BenchmarkSession 1 - M -> Evaluation

Step 4: Design the Database

ModelType
---------------
PK  Id		      INT
	  Name		    VARCHAR(100)
	  Description	TEXT

Functionality
---------------
PK	 Id		        INT
FK	 ModelTypeId	INT
     Description	TEXT
	   Name		      VARCHAR(100)

Architecture
---------------
PK	Id		INT
	Name		VARCHAR(100)
	Description	TEXT

Model
---------------
PK	Id		INT
FK	FunctionalityId	INT
FK	ArchitectureId	INT
	Name		VARCHAR(100)
	Version		VARCHAR(50)
	Description	TEXT

Subject
---------------
PK	Id		INT
	Name		VARCHAR(100)
	Description	TEXT

Topic
---------------
PK	Id		INT
FK	SubjectId	INT
	Name		VARCHAR(100)
	Description	TEXT


Dataset
---------------
PK	Id		INT 
FK	TopicId		INT
	Name 		VARCHAR(100)
	DataType	VARCHAR(50)
	Description	TEXT

Question
---------------
PK	Id			INT
FK	TopicId			INT
FK	DatasetId		INT
	QuestionType		enum
	EstimatedDifficulty	INT
	TestedDifficulty	INT
	Prompt 			TEXT

ExpectedOutput
---------------
PK	Id		INT
FK	QuestionId	INT
	OutputData	JSON

BenchmarkSession
---------------
PK	Id		INT
FK	ModelId		INT
FK	DatasetId	INT
	StartTime	DATETIME
	EndTime		DATETIME
	Status		VARCHAR(50)

EvaluationResult
---------------
PK	Id			INT
FK	BenchmarkSessionId	INT
FK	QuestionId		INT
FK	ExpectedOutputId	INT
	ModelResponse		JSON
	ResponseTimeMS		INT
	Score			DECIMAL(5, 2)
	IsCorrect		BIT
	CreatedAt		DATETIME

Step 5: Design APIs before coding
Run Benchmark
---------------
POST /api/benchmarksessions
GET /api/benchmarksessions
GET /api/benchmarksessions/{id}
GET /api/benchmarksessions/{id}/results
POST /api/benchmarksessions/{id}/start
POST /api/benchmarksessions/{id}/finish

Submit Model Response
---------------
POST /api/benchmarksessions/{id}/responses

Analytics
---------------
GET /api/models/{id}/analytics/

Model Comparison
---------------
GET /api/models/{id}/analytics/compare?first={id}&second={id}

ModelType
---------------
POST /api/modeltypes
GET /api/modeltypes
GET /api/modeltypes/{id}
DELETE /api/modeltypes/{id}

Functionality
---------------
POST /api/functionalities
GET /api/functionalities
GET /api/functionalities/{id}
DELETE /api/functionalities/{id}

Model
---------------
POST /api/models
GET /api/models
GET /api/models/{id}
DELETE /api/models/{id}
PATCH /api/models/{id}
PUT /api/models/{id}

Architecture
---------------
POST /api/architectures
GET /api/architectures
GET /api/architectures/{id} 
DELETE /api/architectures/{id}

Subject
---------------
POST /api/subjects
GET /api/subjects
GET /api/subjects/{id} 
DELETE /api/subjects/{id}

Topic
---------------
POST /api/subjects/{id}/topics
GET /api/subjects/{id}/topics
GET /api/subjects/{id}/topics/{id}
GET /api/subjects/{id}/topics/{id}/questions
DELETE /api/subjects/{id}/topics/{id}

Dataset
---------------
POST /api/topics/{id}/datasets
GET /api/topics/{id}/datasets
GET /api/topics/{id}/datasets/{id} 
DELETE /api/topics/{id}/datasets/{id}

Question
---------------
POST /api/questions
GET /api/questions
GET /api/questions/{id} 
DELETE /api/questions/{id}

ExpectedOutput
---------------
POST /api/questions/{id}/expectedoutputs
GET /api/questions/{id}/expectedoutputs
GET /api/questions/{id}/expectedoutputs/{id} 
DELETE /api/questions/{id}/expectedoutputs/{id}

BenchmarkSession
---------------
POST /api/benchmarksessions
GET /api/benchmarksessions
GET /api/benchmarksessions/{id} 
DELETE /api/benchmarksessions/{id}

EvaluationResult
---------------
POST /api/evaluationresults
GET /api/evaluationresults
GET /api/evaluationresults/{id} 
DELETE /api/evaluationresults/{id}
