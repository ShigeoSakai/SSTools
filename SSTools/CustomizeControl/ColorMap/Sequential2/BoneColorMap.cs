﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential2
{
	/// <summary>
	/// Boneカラーマップ
	/// </summary>
	public class BoneColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BoneColorMap()
		{
			colorMap = bone_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] bone_map_ =
		{
			Color.FromArgb(0,0,0),		// 0
			Color.FromArgb(0,0,1),		// 1
			Color.FromArgb(1,1,2),		// 2
			Color.FromArgb(2,2,3),		// 3
			Color.FromArgb(3,3,4),		// 4
			Color.FromArgb(4,4,6),		// 5
			Color.FromArgb(5,5,7),		// 6
			Color.FromArgb(6,6,8),		// 7
			Color.FromArgb(7,6,9),		// 8
			Color.FromArgb(7,7,10),		// 9
			Color.FromArgb(8,8,12),		// 10
			Color.FromArgb(9,9,13),		// 11
			Color.FromArgb(10,10,14),		// 12
			Color.FromArgb(11,11,15),		// 13
			Color.FromArgb(12,12,17),		// 14
			Color.FromArgb(13,13,18),		// 15
			Color.FromArgb(14,13,19),		// 16
			Color.FromArgb(14,14,20),		// 17
			Color.FromArgb(15,15,21),		// 18
			Color.FromArgb(16,16,23),		// 19
			Color.FromArgb(17,17,24),		// 20
			Color.FromArgb(18,18,25),		// 21
			Color.FromArgb(19,19,26),		// 22
			Color.FromArgb(20,20,28),		// 23
			Color.FromArgb(21,20,29),		// 24
			Color.FromArgb(21,21,30),		// 25
			Color.FromArgb(22,22,31),		// 26
			Color.FromArgb(23,23,32),		// 27
			Color.FromArgb(24,24,34),		// 28
			Color.FromArgb(25,25,35),		// 29
			Color.FromArgb(26,26,36),		// 30
			Color.FromArgb(27,27,37),		// 31
			Color.FromArgb(28,27,38),		// 32
			Color.FromArgb(28,28,40),		// 33
			Color.FromArgb(29,29,41),		// 34
			Color.FromArgb(30,30,42),		// 35
			Color.FromArgb(31,31,43),		// 36
			Color.FromArgb(32,32,45),		// 37
			Color.FromArgb(33,33,46),		// 38
			Color.FromArgb(34,34,47),		// 39
			Color.FromArgb(35,34,48),		// 40
			Color.FromArgb(35,35,49),		// 41
			Color.FromArgb(36,36,51),		// 42
			Color.FromArgb(37,37,52),		// 43
			Color.FromArgb(38,38,53),		// 44
			Color.FromArgb(39,39,54),		// 45
			Color.FromArgb(40,40,56),		// 46
			Color.FromArgb(41,41,57),		// 47
			Color.FromArgb(42,41,58),		// 48
			Color.FromArgb(42,42,59),		// 49
			Color.FromArgb(43,43,60),		// 50
			Color.FromArgb(44,44,62),		// 51
			Color.FromArgb(45,45,63),		// 52
			Color.FromArgb(46,46,64),		// 53
			Color.FromArgb(47,47,65),		// 54
			Color.FromArgb(48,48,66),		// 55
			Color.FromArgb(49,48,68),		// 56
			Color.FromArgb(49,49,69),		// 57
			Color.FromArgb(50,50,70),		// 58
			Color.FromArgb(51,51,71),		// 59
			Color.FromArgb(52,52,73),		// 60
			Color.FromArgb(53,53,74),		// 61
			Color.FromArgb(54,54,75),		// 62
			Color.FromArgb(55,55,76),		// 63
			Color.FromArgb(56,55,77),		// 64
			Color.FromArgb(56,56,79),		// 65
			Color.FromArgb(57,57,80),		// 66
			Color.FromArgb(58,58,81),		// 67
			Color.FromArgb(59,59,82),		// 68
			Color.FromArgb(60,60,84),		// 69
			Color.FromArgb(61,61,85),		// 70
			Color.FromArgb(62,62,86),		// 71
			Color.FromArgb(63,62,87),		// 72
			Color.FromArgb(63,63,88),		// 73
			Color.FromArgb(64,64,90),		// 74
			Color.FromArgb(65,65,91),		// 75
			Color.FromArgb(66,66,92),		// 76
			Color.FromArgb(67,67,93),		// 77
			Color.FromArgb(68,68,94),		// 78
			Color.FromArgb(69,69,96),		// 79
			Color.FromArgb(70,69,97),		// 80
			Color.FromArgb(70,70,98),		// 81
			Color.FromArgb(71,71,99),		// 82
			Color.FromArgb(72,72,101),		// 83
			Color.FromArgb(73,73,102),		// 84
			Color.FromArgb(74,74,103),		// 85
			Color.FromArgb(75,75,104),		// 86
			Color.FromArgb(76,76,105),		// 87
			Color.FromArgb(77,76,107),		// 88
			Color.FromArgb(77,77,108),		// 89
			Color.FromArgb(78,78,109),		// 90
			Color.FromArgb(79,79,110),		// 91
			Color.FromArgb(80,80,112),		// 92
			Color.FromArgb(81,81,113),		// 93
			Color.FromArgb(82,82,114),		// 94
			Color.FromArgb(83,83,114),		// 95
			Color.FromArgb(84,84,115),		// 96
			Color.FromArgb(84,86,116),		// 97
			Color.FromArgb(85,87,117),		// 98
			Color.FromArgb(86,88,118),		// 99
			Color.FromArgb(87,89,119),		// 100
			Color.FromArgb(88,90,120),		// 101
			Color.FromArgb(89,92,121),		// 102
			Color.FromArgb(90,93,121),		// 103
			Color.FromArgb(91,94,122),		// 104
			Color.FromArgb(91,95,123),		// 105
			Color.FromArgb(92,96,124),		// 106
			Color.FromArgb(93,98,125),		// 107
			Color.FromArgb(94,99,126),		// 108
			Color.FromArgb(95,100,127),		// 109
			Color.FromArgb(96,101,128),		// 110
			Color.FromArgb(97,102,128),		// 111
			Color.FromArgb(98,104,129),		// 112
			Color.FromArgb(98,105,130),		// 113
			Color.FromArgb(99,106,131),		// 114
			Color.FromArgb(100,107,132),		// 115
			Color.FromArgb(101,109,133),		// 116
			Color.FromArgb(102,110,134),		// 117
			Color.FromArgb(103,111,135),		// 118
			Color.FromArgb(104,112,135),		// 119
			Color.FromArgb(105,113,136),		// 120
			Color.FromArgb(105,115,137),		// 121
			Color.FromArgb(106,116,138),		// 122
			Color.FromArgb(107,117,139),		// 123
			Color.FromArgb(108,118,140),		// 124
			Color.FromArgb(109,119,141),		// 125
			Color.FromArgb(110,121,142),		// 126
			Color.FromArgb(111,122,142),		// 127
			Color.FromArgb(112,123,143),		// 128
			Color.FromArgb(112,124,144),		// 129
			Color.FromArgb(113,125,145),		// 130
			Color.FromArgb(114,127,146),		// 131
			Color.FromArgb(115,128,147),		// 132
			Color.FromArgb(116,129,148),		// 133
			Color.FromArgb(117,130,149),		// 134
			Color.FromArgb(118,131,149),		// 135
			Color.FromArgb(119,133,150),		// 136
			Color.FromArgb(119,134,151),		// 137
			Color.FromArgb(120,135,152),		// 138
			Color.FromArgb(121,136,153),		// 139
			Color.FromArgb(122,137,154),		// 140
			Color.FromArgb(123,139,155),		// 141
			Color.FromArgb(124,140,156),		// 142
			Color.FromArgb(125,141,156),		// 143
			Color.FromArgb(126,142,157),		// 144
			Color.FromArgb(126,143,158),		// 145
			Color.FromArgb(127,145,159),		// 146
			Color.FromArgb(128,146,160),		// 147
			Color.FromArgb(129,147,161),		// 148
			Color.FromArgb(130,148,162),		// 149
			Color.FromArgb(131,149,163),		// 150
			Color.FromArgb(132,151,163),		// 151
			Color.FromArgb(133,152,164),		// 152
			Color.FromArgb(133,153,165),		// 153
			Color.FromArgb(134,154,166),		// 154
			Color.FromArgb(135,155,167),		// 155
			Color.FromArgb(136,157,168),		// 156
			Color.FromArgb(137,158,169),		// 157
			Color.FromArgb(138,159,170),		// 158
			Color.FromArgb(139,160,170),		// 159
			Color.FromArgb(140,161,171),		// 160
			Color.FromArgb(140,163,172),		// 161
			Color.FromArgb(141,164,173),		// 162
			Color.FromArgb(142,165,174),		// 163
			Color.FromArgb(143,166,175),		// 164
			Color.FromArgb(144,167,176),		// 165
			Color.FromArgb(145,169,177),		// 166
			Color.FromArgb(146,170,177),		// 167
			Color.FromArgb(147,171,178),		// 168
			Color.FromArgb(147,172,179),		// 169
			Color.FromArgb(148,173,180),		// 170
			Color.FromArgb(149,175,181),		// 171
			Color.FromArgb(150,176,182),		// 172
			Color.FromArgb(151,177,183),		// 173
			Color.FromArgb(152,178,184),		// 174
			Color.FromArgb(153,179,184),		// 175
			Color.FromArgb(154,181,185),		// 176
			Color.FromArgb(154,182,186),		// 177
			Color.FromArgb(155,183,187),		// 178
			Color.FromArgb(156,184,188),		// 179
			Color.FromArgb(157,186,189),		// 180
			Color.FromArgb(158,187,190),		// 181
			Color.FromArgb(159,188,191),		// 182
			Color.FromArgb(160,189,191),		// 183
			Color.FromArgb(161,190,192),		// 184
			Color.FromArgb(161,192,193),		// 185
			Color.FromArgb(162,193,194),		// 186
			Color.FromArgb(163,194,195),		// 187
			Color.FromArgb(164,195,196),		// 188
			Color.FromArgb(165,196,197),		// 189
			Color.FromArgb(166,198,198),		// 190
			Color.FromArgb(167,199,198),		// 191
			Color.FromArgb(168,199,199),		// 192
			Color.FromArgb(170,200,200),		// 193
			Color.FromArgb(171,201,201),		// 194
			Color.FromArgb(172,202,202),		// 195
			Color.FromArgb(174,203,203),		// 196
			Color.FromArgb(175,204,204),		// 197
			Color.FromArgb(177,205,205),		// 198
			Color.FromArgb(178,206,205),		// 199
			Color.FromArgb(179,206,206),		// 200
			Color.FromArgb(181,207,207),		// 201
			Color.FromArgb(182,208,208),		// 202
			Color.FromArgb(183,209,209),		// 203
			Color.FromArgb(185,210,210),		// 204
			Color.FromArgb(186,211,211),		// 205
			Color.FromArgb(188,212,212),		// 206
			Color.FromArgb(189,213,212),		// 207
			Color.FromArgb(190,213,213),		// 208
			Color.FromArgb(192,214,214),		// 209
			Color.FromArgb(193,215,215),		// 210
			Color.FromArgb(194,216,216),		// 211
			Color.FromArgb(196,217,217),		// 212
			Color.FromArgb(197,218,218),		// 213
			Color.FromArgb(198,219,219),		// 214
			Color.FromArgb(200,220,219),		// 215
			Color.FromArgb(201,220,220),		// 216
			Color.FromArgb(203,221,221),		// 217
			Color.FromArgb(204,222,222),		// 218
			Color.FromArgb(205,223,223),		// 219
			Color.FromArgb(207,224,224),		// 220
			Color.FromArgb(208,225,225),		// 221
			Color.FromArgb(209,226,226),		// 222
			Color.FromArgb(211,227,226),		// 223
			Color.FromArgb(212,227,227),		// 224
			Color.FromArgb(213,228,228),		// 225
			Color.FromArgb(215,229,229),		// 226
			Color.FromArgb(216,230,230),		// 227
			Color.FromArgb(218,231,231),		// 228
			Color.FromArgb(219,232,232),		// 229
			Color.FromArgb(220,233,233),		// 230
			Color.FromArgb(222,234,233),		// 231
			Color.FromArgb(223,234,234),		// 232
			Color.FromArgb(224,235,235),		// 233
			Color.FromArgb(226,236,236),		// 234
			Color.FromArgb(227,237,237),		// 235
			Color.FromArgb(229,238,238),		// 236
			Color.FromArgb(230,239,239),		// 237
			Color.FromArgb(231,240,240),		// 238
			Color.FromArgb(233,241,240),		// 239
			Color.FromArgb(234,241,241),		// 240
			Color.FromArgb(235,242,242),		// 241
			Color.FromArgb(237,243,243),		// 242
			Color.FromArgb(238,244,244),		// 243
			Color.FromArgb(239,245,245),		// 244
			Color.FromArgb(241,246,246),		// 245
			Color.FromArgb(242,247,247),		// 246
			Color.FromArgb(244,248,247),		// 247
			Color.FromArgb(245,248,248),		// 248
			Color.FromArgb(246,249,249),		// 249
			Color.FromArgb(248,250,250),		// 250
			Color.FromArgb(249,251,251),		// 251
			Color.FromArgb(250,252,252),		// 252
			Color.FromArgb(252,253,253),		// 253
			Color.FromArgb(253,254,254),		// 254
			Color.FromArgb(255,255,255),		// 255
		};
	}
}
